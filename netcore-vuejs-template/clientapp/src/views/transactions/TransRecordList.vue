<template>
  <div>
    <div class="mb-10 mx-2 mt-4">
      <v-data-table
        ref="table"
        v-model="checkedRows"
        :items="transRecordList"
        item-key="TransactionId"
        :headers="tblHeaders"
        :footer-props="{
          itemsPerPageOptions: [100, 200, 300, -1]
        }"
        show-select
        dense
        sort-by="TransactionId"
        :sort-desc="true"
        :search="search2"
        :page.sync="page"
        :items-per-page="itemsPerPage"
        hide-default-footer
        @page-count="pageCount = $event"
      >
        <template v-slot:top="props">
          <div class="">
            <v-row>
              <v-col cols="8" lg="8" md="5" sm="8">
                <v-text-field
                  v-model="search2"
                  solo=""
                  rounded
                  label="Search by Name"
                  color="grey"
                  style="max-width:240px"
                  dense
                  hide-details
                  append-icon="mdi-magnify"
                  :show-select="true"
                  class="mr-3 d-inline-block"
                ></v-text-field>
                <v-badge bordered color="error" :value="isFiltered" overlap dot>
                  <v-btn
                    fab
                    small
                    class="elevation-2  "
                    @click="showFilter = true"
                    ><v-icon>mdi-filter-variant</v-icon></v-btn
                  >
                </v-badge>
                <v-btn
                  fab
                  small
                  class="elevation-2 ml-2"
                  dark
                  color="red lighten-2"
                  @click="showFilter = true"
                  ><v-icon>mdi-delete</v-icon></v-btn
                >
                <v-btn
                  fab
                  small
                  class="elevation-2 ml-2"
                  dark
                  color="red lighten-2"
                  :to="{
                    name: 'TransactionWeighIn'
                  }"
                  ><v-icon>mdi-plus</v-icon></v-btn
                >
              </v-col>
              <v-col cols="4" lg="4" md="12" sm="12" class="text-right">
                <div class="">
                  <Pagination
                    v-model="page"
                    :page-count="pageCount"
                    :page-size="itemsPerPage"
                    :items-length="props.pagination.itemsLength"
                    class="text-right "
                  />
                </div>
                <v-chip small
                  ><span class="mr-2">From </span>
                  <strong>{{ transRecordFilter.DTInboundFrom }}</strong>
                  <span class="mx-2">To </span>
                  <strong>{{ transRecordFilter.DTInboundTo }}</strong></v-chip
                >
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" class="text-right">
                <v-spacer></v-spacer>
              </v-col>
            </v-row>
          </div>
        </template>
        <template v-slot:[`item.TransactionId`]="{ item }">
          <v-btn
            dark
            color="success"
            fab
            x-small
            icon
            class="elevation-1 my-1 mr-2"
            :to="{
              name: 'TransactionWeighOut',
              params: { id: parseInt(item.TransactionId) }
            }"
          >
            <v-icon small>mdi-arrow-right-bold</v-icon>
          </v-btn>
          <v-btn
            fab
            icon
            x-small
            class="elevation-1 my-1"
            :to="{
              name: 'TransactionUpdate',
              params: { id: parseInt(item.TransactionId) }
            }"
          >
            <v-icon small>mdi-pencil</v-icon>
          </v-btn>
        </template>
        <template v-slot:[`item.DT`]="{ item }">
          <span class="caption">{{ item.DT }}</span>
        </template>
        <template v-slot:[`item.RawMaterialDesc`]="{ item }">
          <span class="caption ellipsis" style="width:100px">{{
            item.RawMaterialDesc
          }}</span>
        </template>
        <template v-slot:[`item.PalletNum`]="{ item }">
          <span class="caption">{{ item.PalletNum }}</span>
        </template>
        <template v-slot:[`item.BinLocDesc`]="{ item }">
          <span class="caption ellipsis" style="max-width:100px">{{
            item.BinLocDesc
          }}</span>
        </template>
        <template v-slot:[`item.GrossWt`]="{ item }">
          <span class="caption font-weight-bold">{{
            item.GrossWt | toCommaNumeral
          }}</span>
        </template>
        <template v-slot:[`item.TareWt`]="{ item }">
          <span class="caption font-weight-bold">{{
            item.TareWt | toCommaNumeral
          }}</span>
        </template>
        <template v-slot:[`item.NetWt`]="{ item }">
          <span class="caption font-weight-bold">{{
            item.NetWt | toCommaNumeral
          }}</span>
        </template>
        <template v-slot:[`item.WeigherInName`]="{ item }">
          <span class="caption ellipsis">{{ item.WeigherInName }}</span>
        </template>
      </v-data-table>
    </div>
    <TransRecordListFilter
      v-model="showFilter"
      :trans-record-filter="transRecordFilter"
      :apply-func="init"
    />
  </div>
</template>

<script>
import { EventBus } from "@/main.js";
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import transRecordService from "@/services/transRecordService.js";
import filters from "@/helpers/filters.js";

const today = new Date().toISOString().substr(0, 10);

export default {
  name: "TransRecordList",
  filters: {
    ...filters
  },
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      showFilter: false,
      search2: "",
      checkedRows: [],
      pageTitle: "Transactions",
      page: 1,
      pageCount: 0,
      itemsPerPage: 100,
      transRecordList: [],
      dTInboundFromMenu: false,
      dTInboundToMenu: false,
      filterMenu: false,
      transRecordFilter: {
        TransactionStatus: "PENDING",
        FilterInboundDate: true,
        DTInboundFrom: today,
        DTInboundTo: today,
        ShiftId: 0,
        ShiftDesc: "ALL",
        RawMaterialId: [],
        BinLocationId: [],
        PalletNum: "",
        BinNum: ""
      },
      tblHeaders: [
        {
          text: "Inbound Date",
          value: "DT",
          sortable: true,
          align: "left",
          width: 170
        },
        {
          text: "Material",
          value: "RawMaterialDesc",
          sortable: true,
          align: "left",
          width: 100
        },
        {
          text: "Line",
          value: "BinLocDesc",
          sortable: true,
          align: "left",
          width: 100
        },
        {
          text: "Pallet #",
          value: "PalletNum",
          sortable: true,
          align: "left",
          width: 140
        },
        {
          text: "Gross",
          value: "GrossWt",
          sortable: false,
          align: "right"
        },
        {
          text: "Tare",
          value: "TareWt",
          sortable: false,
          align: "right"
        },
        {
          text: "Net",
          value: "NetWt",
          sortable: false,
          align: "right"
        },
        {
          text: "Quantity",
          value: "Quantity",
          sortable: false,
          align: "left"
        },
        {
          text: "Control #",
          value: "ControlNum",
          sortable: true,
          align: "left",
          width: 100
        },
        {
          text: "Weigh-In By",
          value: "WeigherInName",
          sortable: false,
          align: "left"
        },
        {
          text: "",
          value: "TransactionId",
          sortable: false,
          align: "right",
          width: 120
        }
      ]
    };
  },

  computed: {
    isFiltered() {
      if (
        this.transRecordFilter.ShiftId != 0 ||
        this.transRecordFilter.RawMaterialId.length > 0 ||
        this.transRecordFilter.PalletNum != ""
      ) {
        console.log("FILTERED");
        return true;
      } else {
        console.log("NOT FILTERED");
        return false;
      }
    }
  },
  watch: {
    // "transRecordFilter.TransactionStatus": {
    //   handler: "changeTransStatus",
    //   deep: true
    // }
  },
  async created() {
    await this.init();
    EventBus.$emit("hideDrawer");
  },
  methods: {
    // changeTransStatus(val, oldval) {
    //   if (val != oldval) this.init();
    // },
    async init() {
      try {
        var pageTitle = "";
        const response = await transRecordService.list(this.transRecordFilter);
        if (this.transRecordFilter.TransactionStatus == "PENDING") {
          pageTitle = "Pending Transactions";
          this.tblHeaders[0].text = "Inbound Date";
        } else {
          pageTitle = "Completed Transactions";
          this.tblHeaders[0].text = "Outbound Date";
        }

        EventBus.$emit("setPageTitle", pageTitle);

        this.transRecordList = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  }
};
</script>

<style></style>
