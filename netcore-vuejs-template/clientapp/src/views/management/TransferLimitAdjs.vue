<template>
  <div class="pa-10">
    <v-dialog
      v-model="show"
      max-height="600px"
      fullscreen
      hide-overlay
      transition="dialog-bottom-transition"
    >
      <v-card>
        <v-toolbar dark color="grey darken-3" tile>
          <v-btn icon dark @click="close()">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>Adjustments</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items> </v-toolbar-items>
        </v-toolbar>
        <v-card-text>
          <v-divider class="mb-4"></v-divider>
          <div class="d-flex justify-space-between media">
            <div>
              <div class="grey--text text--darken-1 sub-title">
                Effective Date
              </div>
              <h4 class="d-inline-block grey--text text--darken-3  title">
                {{ transferLimit.EffectiveDate | moment("MMM DD, YYYY") }}
              </h4>
              <div
                class="d-block grey--text text--darken-4 sub-title"
                v-text="transferLimit.ShiftDesc"
              ></div>
            </div>
            <v-divider vertical class="mx-4"></v-divider>
            <div>
              <div class="grey--text text--darken-1 sub-title">
                Material
              </div>
              <h4
                class="d-inline-block grey--text text--darken-3  title"
                v-text="transferLimit.RawMaterialDesc"
              ></h4>
            </div>
            <v-divider vertical class="mx-4"></v-divider>
            <div class="text-right">
              <div class="grey--text text--darken-1 sub-title ">
                Initial Computed Limit
              </div>
              <div>
                <ChiptransferLimitStatus :status="status" />
                <h4 class="d-inline-block grey--text text--darken-3 title">
                  {{ transferLimit.ComputedLimitKg }} Kg
                </h4>
              </div>
            </div>
          </div>
          <v-data-table
            v-model="checkedRows"
            item-key="TransferLimitAdjId"
            :headers="tblHeaders"
            :items="items"
            :search="search"
            sort-by="AdjDate"
            :sort-desc="true"
            show-select
            nowrap
          >
            <template v-slot:top>
              <v-toolbar class="px-0" flat>
                <v-spacer></v-spacer>
                <ButtonDelete @click="remove()" />
                <ButtonAdd
                  :to="{
                    name: 'TransferLimitAdjCreate',
                    params: { transferLimitId: transferLimit.TransferLimitId }
                  }"
                />
              </v-toolbar>
            </template>
            <template v-slot:[`item.IsActive`]="{ item }">
              <v-chip v-if="item.IsActive" class="mr-4" color="primary">
                Active
              </v-chip>
            </template>
            <template v-slot:[`item.TransferLimitAdjId`]="{ item }">
              <v-btn
                fab
                x-small
                class="elevation-1"
                :to="{
                  name: 'TransferLimitAdjUpdate',
                  params: {
                    transferLimitId: transferLimit.TransferLimitId,
                    id: item.TransferLimitAdjId
                  }
                }"
              >
                <v-icon color="success">mdi-pencil</v-icon>
              </v-btn>
            </template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-dialog>
    <router-view></router-view>
    <MessageBox ref="messagebox" />
  </div>
</template>

<script>
import Vue from "vue";
Vue.use(require("vue-moment"));

import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import tLimitAdjsService from "../../services/transferLimitAdjService";
import { mapActions } from "vuex";

export default {
  name: "TransferLimitAdjs",
  mixins: [navbarMixins, notiMixin],
  props: {
    effectiveDate: {
      type: String,
      default: ""
    },
    rawMaterialId: {
      type: [Number, String],
      default: 0
    },
    shiftId: {
      type: [Number, String],
      default: 1
    }
  },
  data() {
    return {
      pageTitle: "Transfer Limit Adjustments",
      show: false,
      checkedRows: [],
      search: "",
      tblHeaders: [
        {
          text: "Date",
          value: "AdjDate",
          align: "left",
          sortable: false,
          width: 200
        },
        {
          text: "Adjustment (Kg)",
          value: "AdjLimit",
          sortable: false,
          align: "left",
          width: 150
        },
        {
          text: "",
          value: "IsActive",
          align: "center",
          width: 100
        },
        {
          text: "Remarks",
          value: "AdjRemarks",
          align: "left",
          sortable: false
        },
        {
          text: "action",
          value: "TransferLimitAdjId",
          align: "right"
        }
      ],
      transferLimit: {},
      items: []
    };
  },
  computed: {
    status() {
      return {
        EffectiveDate: this.transferLimit.EffectiveDate,
        RawMaterialId: this.transferLimit.RawMaterialId,
        ComputedLimitKg: this.transferLimit.ComputedLimitKg,
        LimitStatus: this.transferLimit.LimitStatus,
        ShiftId: this.transferLimit.ShiftId
      };
    }
  },
  created() {
    this.init();
  },
  methods: {
    ...mapActions("transferLimit", ["listTransferLimit"]),
    async init() {
      try {
        this.checkedRows = [];
        const response = await tLimitAdjsService.list({
          effectiveDate: this.effectiveDate,
          rawMaterialId: this.rawMaterialId,
          shiftId: this.shiftId
        });

        const {
          TransferLimitViewModel,
          TransferLimitAdjCollection
        } = response.data;

        this.transferLimit = TransferLimitViewModel;
        this.items = TransferLimitAdjCollection;

        this.show = true;
      } catch (error) {
        this.showNotification({
          type: "error",
          content: error
        });
        this.close();
      }
    },
    async remove() {
      if (this.checkedRows.length == 0) return;
      const count = this.checkedRows.length;

      const msgBoxOpts = {
        title: "Delete Confirmation",
        content: `Do you want to delete selected items(${count})`,
        buttons: "deleteCancel"
      };

      this.$refs.messagebox.open(msgBoxOpts, async () => {
        try {
          let params = this.checkedRows.map(a => a.TransferLimitAdjId);
          await tLimitAdjsService.bulkDelete(params);
          await this.init();
          await this.listTransferLimit();
          this.checkedRows = [];

          this.showNotification({
            type: "success",
            content: `${params.length} ${
              params.length == 1 ? "record" : "records"
            } deleted successfully`
          });
        } catch (error) {
          this.showNotification({
            type: "error",
            content: error
          });
        }
      });
    },
    formatDate(dt) {
      return new Date(dt).toISOString().substr(0, 10);
    },
    close() {
      this.show = false;
      this.$router.back();
    }
  },
  beforeRouteUpdate(to, from, next) {
    if (from.meta.cancelled === false) this.init();
    next();
  }
};
</script>

<style lang="scss">
.media {
  & > div:nth-of-type(3) {
    flex: 1;
  }
}
</style>
