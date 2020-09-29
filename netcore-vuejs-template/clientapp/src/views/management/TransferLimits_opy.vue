<template>
  <v-sheet class="pa-10">
    <v-toolbar
      style="position: sticky; z-index:100 ; top: 65px;"
      dark
      color="grey darken-3"
      class="mb-1"
      tile
    >
      <v-menu
        v-model="dtPickerMenu"
        :close-on-content-click="false"
        transition="scale-transition"
        offset-y
        min-width="290px"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="selectedDate"
            label="Picker in menu"
            prepend-icon="mdi-calendar"
            readonly
            hide-details
            v-bind="attrs"
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker
          v-model="selectedDate"
          no-title
          scrollable
          @input="refreshList"
        >
        </v-date-picker>
      </v-menu>
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        label="Search "
        style="max-width:320px"
        color="grey"
        prepend-icon="mdi-search"
        hide-details
        :show-select="true"
        @input="refreshList()"
      ></v-text-field>
    </v-toolbar>
    <v-data-iterator
      id="transferlimits_data"
      :items="items"
      item-key="RawMaterialId"
      :items-per-page="-1"
      hide-default-footer
      :search="search"
    >
      <template v-slot:header>
        <v-sheet
          class="v-data-iterator__header px-3"
          style="position: sticky; z-index:100 ; top: 125px;"
          tile
        >
          <v-row>
            <v-col cols="6" class="title"> Description</v-col>
            <v-col cols="3" class="title text-right"> Day Shift </v-col>
            <v-col cols="3" class="title text-right"> Night Shift</v-col>
          </v-row>
          <v-divider></v-divider>
        </v-sheet>
      </template>
      <template v-slot:default="props">
        <div>
          <div
            v-for="item in props.items"
            :key="item.RawMaterialId"
            class="v-data-iterator__row"
          >
            <v-row>
              <v-col cols="6">
                {{ item.RawMaterialDesc }}
              </v-col>
              <v-col
                cols="3"
                :class="
                  `col-shift col-shift__day ${getShiftStatusClass(
                    item.LimitStatusShift1
                  )}`
                "
              >
                <v-edit-dialog
                  :return-value.sync="item.LimitShift1"
                  large
                  persistent
                  @save="save"
                  @cancel="cancel"
                  @open="open"
                  @close="close"
                >
                  <v-sheet class="col-shift__content" tile>
                    <v-tooltip
                      v-model="showtooltip"
                      bottom
                      small
                      class="shift-status__tooltip"
                    >
                      <template v-slot:activator="{ on }">
                        <v-icon
                          class="shift-status__icon"
                          v-bind="focus"
                          v-on="on"
                          >{{
                            getShiftStatusIcon(item.LimitStatusShift1)
                          }}</v-icon
                        >
                      </template>
                      <span class="shift-status__tooltip__content">{{
                        getShiftStatusToolTipMsg(item.LimitStatusShift1)
                      }}</span>
                    </v-tooltip>
                    <v-tooltip v-model="showtooltip" bottom>
                      <template v-slot:activator="{ on }">
                        <v-icon
                          v-show="isAdjusted(item.AdjRemarksShift1)"
                          class="shift-status__adjusted"
                          small
                          v-bind="focus"
                          v-on="on"
                          >mdi-adjust</v-icon
                        >
                      </template>
                      <span>Adjusted</span>
                    </v-tooltip>
                    <span class="col-shift__limit">{{
                      item.LimitShift1 | toCommaNumeral
                    }}</span>
                  </v-sheet>
                  <template v-slot:input>
                    <div class="mt-4 title">Shift 1 Limit</div>
                    <v-text-field
                      v-model.number="item.LimitShift1"
                      label="Edit"
                      single-line
                      autofocus
                      type="number"
                    ></v-text-field>
                  </template>
                </v-edit-dialog>
                <v-btn icon class="col-shift__more">
                  <v-icon>mdi-dots-vertical</v-icon>
                </v-btn>
              </v-col>
              <v-col
                cols="3"
                :class="
                  `col-shift col-shift__night ${getShiftStatusClass(
                    item.LimitStatusShift2
                  )}`
                "
              >
                <v-edit-dialog
                  :return-value.sync="item.LimitShift2"
                  large
                  persistent
                  @save="save"
                  @cancel="cancel"
                  @open="open"
                  @close="close"
                >
                  <v-sheet class="col-shift__content" tile>
                    <v-icon class="shift-status__icon">{{
                      getShiftStatusIcon(item.LimitStatusShift2)
                    }}</v-icon>
                    <v-icon
                      v-show="isAdjusted(item.AdjRemarksShift2)"
                      class="shift-status__adjusted"
                      >mdi-check</v-icon
                    >
                    <span class="col-shift__limit">{{
                      item.LimitShift2 | toCommaNumeral
                    }}</span>
                  </v-sheet>
                  <template v-slot:input>
                    <div class="mt-4 title">Shift 2 Limit</div>
                    <v-text-field
                      v-model.number="item.LimitShift2"
                      label="Edit"
                      single-line
                      autofocus
                      type="number"
                    ></v-text-field>
                  </template>
                </v-edit-dialog>
                <v-btn icon class="col-shift__more">
                  <v-icon>mdi-dots-vertical</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </div>
        </div>
      </template>
    </v-data-iterator>
    <router-view></router-view>
    <MessageBox ref="messagebox" />
  </v-sheet>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import transferLimitService from "@/services/transferLimitService.js";
import filters from "@/helpers/filters.js";

export default {
  name: "TransferLimits1",
  filters: {
    ...filters
  },
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Transfer Limits",
      selectedDate: new Date().toISOString().substr(0, 10),
      dtPickerMenu: false,
      checkedRows: [],
      search: "",
      items: [],
      tblHeaders: [
        {
          text: "Description",
          value: "RawMaterialDesc",
          sortable: true,
          align: "left"
        },
        {
          text: "Day Shift",
          value: "LimitShift1",
          align: "right",
          sortable: false
        },
        {
          text: "Night Shift",
          value: "LimitShift2",
          align: "right",
          sortable: false
        }
      ],
      stickyOptions: {
        marginTop: 20,
        forName: 0,
        className: "stuck"
      }
    };
  },
  async created() {
    this.checkedRows = [];
    await this.init();
  },
  methods: {
    async init() {
      try {
        await this.refreshList();
      } catch (error) {
        console.log(error);
      }
    },
    save() {},
    cancel() {},
    open() {},
    close() {},
    getShiftStatusClass(status) {
      switch (status) {
        case 1:
          return "shift-status--warning";
        case 2:
          return "shift-status--invalid";
        default:
          return "";
      }
    },
    getShiftStatusIcon(status) {
      switch (status) {
        case 1:
          return "mdi-exclamation-thick";
        case 2:
          return "mdi-alert";
        default:
          return "";
      }
    },
    getShiftStatusToolTipMsg(status) {
      console.log(status);
      switch (status) {
        case 1:
          return "Warning";
        case 2:
          return "Limit Reached";
        default:
          return "";
      }
    },
    isAdjusted(adjRemarks) {
      return adjRemarks === null;
    },
    async refreshList() {
      this.dtPickerMenu = false;
      const response = await transferLimitService.list(this.selectedDate);
      this.items = response.data;
    }
  },

  beforeRouteUpdate(to, from, next) {
    if (!from.meta.cancelled == true) this.init();
    next();
  }
};
</script>

<style lang="scss">
$rowEvenBg: #fff;
$rowOddBg: #fafafa;

.v-data-iterator__row > .row {
  margin-left: 0;
  margin-right: 0;
}

.v-data-iterator__row:nth-of-type(even) {
  $rowEvenBg: #fff;
  background-color: $rowEvenBg;

  .col-shift__day,
  .col-shift__day .col-shift__content {
    background-color: darken($rowEvenBg, 4%);
  }

  .col-shift__night,
  .col-shift__night .col-shift__content {
    background-color: darken($rowEvenBg, 8%);
  }
}

.v-data-iterator__row:nth-of-type(odd) {
  background: $rowOddBg;
  .col-shift__day,
  .col-shift__day .col-shift__content {
    background-color: darken($rowOddBg, 4%);
  }
  .col-shift__night,
  .col-shift__night .col-shift__content {
    background-color: darken($rowOddBg, 8%);
  }
}

.col-shift {
  padding-top: 0;
  padding-bottom: 0;
  display: flex;
  flex-basis: 1;
  align-items: center;
  padding-right: 0;
}

.col-shift .v-small-dialog__activator {
  width: 100%;
}

.col-shift .v-small-dialog__activator__content {
  display: flex;
  height: 100%;
}

.col-shift__content {
  padding: 8px;
  flex: 1;
  position: relative;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}

.shift-status--warning {
  background-color: #d4e157 !important;
  .col-shift__content {
    background-color: #d4e157 !important;
  }
  .shift-status__icon {
    color: #f57f17;
  }
  .shift-status__adjusted {
    color: #f57f17;
  }
}

.shift-status--invalid {
  background-color: #ff9100 !important;
  .col-shift__content {
    background-color: #ff9100 !important;
  }
  .shift-status__icon {
    color: #bf360c;
  }
}

.shift-status__tooltip__content {
  font-size: 80%;
}

.col-shift .shift-status__adjusted {
  padding-left: 6px;
  color: #bdbdbd !important;
}

.shift-status--warning .shift-status__adjusted {
  color: #757575 !important;
}

.shift-status--invalid .shift-status__adjusted {
  color: #424242 !important;
}

.col-shift__limit {
  flex-grow: 2;
  text-align: right;
}

.col-shift__more i {
  color: #e0e0e0 !important;
}

.shift-status--warning .col-shift__more i {
  color: #757575 !important;
}

.shift-status--invalid .col-shift__more i {
  color: #424242 !important;
}
</style>
