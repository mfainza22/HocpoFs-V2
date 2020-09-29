<template>
  <v-sheet class="pa-10">
    <div class="parent-route"></div>
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
            v-model="dtPickerDate"
            label="Picker in menu"
            prepend-icon="mdi-calendar"
            readonly
            hide-details
            v-bind="attrs"
            v-on="on"
          ></v-text-field>
        </template>
        <v-date-picker v-model="dtPickerDate" no-title scrollable @input="init">
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
            v-for="(item, index) in props.items"
            :key="`${item.EffectiveDate}_${index}`"
            class="v-data-iterator__row"
          >
            <v-row>
              <v-col cols="6">
                {{ item.RawMaterialDesc }}
              </v-col>
              <v-col cols="3" class="col-shift col-shift__day">
                <ColTransferLimitShift
                  :item="getShiftDay(item)"
                  :save-callback="updateLimit"
                />
              </v-col>
              <v-col cols="3" class="col-shift col-shift__night">
                <ColTransferLimitShift
                  :item="getShiftNight(item)"
                  :save-callback="updateLimit"
                />
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
import { mapState, mapActions, mapGetters } from "vuex";

export default {
  name: "TransferLimits",
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Transfer Limits",
      dtPickerMenu: false,
      dtPickerDate: new Date().toISOString().substr(0, 10),
      checkedRows: [],
      search: "",
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
      ]
    };
  },
  computed: {
    ...mapState("transferLimit", {
      items: state => state.transferLimitList,
      selectedDate: state => state.selectedDate
    }),
    ...mapGetters("transferLimit", ["getShiftDay", "getShiftNight"])
  },
  async created() {
    try {
      await this.init();
    } catch (error) {
      this.showNotification({
        type: "error",
        content: error
      });
    }
  },
  methods: {
    async init() {
      this.dtPickerMenu = false;
      try {
        await this.listTransferLimit(this.dtPickerDate);
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    async updateLimit(item) {
      try {
        await this.updateLimit(item);

        switch (item.ShiftId) {
          case 1:
            this.items[item.index].LimitShift1 = item.ComputedLimitKg;
            break;
          case 2:
            this.items[item.index].LimitShift2 = item.ComputedLimitKg;
            break;
        }

        this.showNotification({
          type: "success",
          content: "Computed Limit Updated."
        });

        await this.init();
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
      }
    },
    ...mapActions("transferLimit", {
      listTransferLimit: "listTransferLimit",
      updateLimit: "updateLimit"
    })
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
  .col-shift__day .shift-status__limit,
  .col-shift__day .shift-status__content {
    background-color: darken($rowEvenBg, 4%);
  }

  .col-shift__night,
  .col-shift__night .shift-status__limit,
  .col-shift__night .shift-status__content {
    background-color: darken($rowEvenBg, 8%);
  }
}

.v-data-iterator__row:nth-of-type(odd) {
  background: $rowOddBg;

  .col-shift__day,
  .col-shift__day .shift-status__limit,
  .col-shift__day .shift-status__content {
    background-color: darken($rowOddBg, 4%);
  }

  .col-shift__night,
  .col-shift__night .shift-status__limit,
  .col-shift__night .shift-status__content {
    background-color: darken($rowOddBg, 8%);
  }
}

.col-shift {
  padding: 0 !important;
  height: 100% !important;
}

.col-shift .v-small-dialog__activator {
  width: 100%;
}

.col-shift .v-small-dialog__activator__content {
  display: flex;
  height: 100%;
}
</style>
