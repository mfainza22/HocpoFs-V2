<template>
  <div>
    <v-dialog
      v-model="show"
      fullscreen
      max-height="600px"
      hide-overlay
      transition="dialog-bottom-transition"
    >
      <v-card tile>
        <v-toolbar flat dark color="primary">
          <v-btn icon dark @click.stop="show = false">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <v-toolbar-title>Filter Options</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark text @click="reset()">
              Reset
            </v-btn>
            <v-btn dark text @click="apply()">
              Apply
            </v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-card-text>
          <v-row>
            <v-col cols="12" lg="4" md="6" sm="8">
              <v-row>
                <v-col cols="12">
                  <v-btn-toggle
                    v-model="transRecordFilter.TransactionStatus"
                    borderless
                    mandatory
                    color="primary accent-3"
                    group
                  >
                    <v-btn value="PENDING">
                      <span class="">PENDING</span>
                      <v-icon class="" right>mdi-arrow-left-bold</v-icon>
                    </v-btn>
                    <v-btn value="COMPLETED">
                      <span class="">COMPLETED</span>
                      <v-icon class="" right>mdi-arrow-right-bold</v-icon>
                    </v-btn>
                  </v-btn-toggle>
                </v-col>
                <v-col cols="12">
                  <div
                    v-if="transRecordFilter.TransactionStatus == 'COMPLETED'"
                    class="d-flex align-center"
                  >
                    <v-menu
                      v-model="dTInboundFromMenu"
                      :close-on-content-click="false"
                      transition="scale-transition"
                      offset-y
                      min-width="290px"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <div>
                          <div class="mr-2">
                            <div>From</div>
                            <v-chip
                              class="pa-4 px-5 subtitle-2"
                              v-bind="attrs"
                              v-on="on"
                              >{{ transRecordFilter.DTInboundFrom }}</v-chip
                            >
                          </div>
                        </div>
                      </template>
                      <v-date-picker
                        v-model="transRecordFilter.DTInboundFrom"
                        no-title
                        scrollable
                        :max="transRecordFilter.DTInboundTo"
                        @input="dTInboundFromMenu = false"
                      >
                      </v-date-picker>
                    </v-menu>
                    <v-menu
                      v-model="dTInboundToMenu"
                      :close-on-content-click="false"
                      transition="scale-transition"
                      offset-y
                      min-width="290px"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <div>
                          <div class="mr-4">
                            <div>To</div>
                            <v-chip
                              class="pa-4 px-5 subtitle-2"
                              v-bind="attrs"
                              v-on="on"
                              >{{ transRecordFilter.DTInboundTo }}</v-chip
                            >
                          </div>
                        </div>
                      </template>
                      <v-date-picker
                        v-model="transRecordFilter.DTInboundTo"
                        no-title
                        scrollable
                        @input="dTInboundToMenu = false"
                      >
                      </v-date-picker>
                    </v-menu>
                    <v-menu>
                      <template v-slot:activator="{ on, attrs }">
                        <div>
                          <div class="mr-4">
                            <div>Shift</div>
                            <v-chip
                              class="pa-4 px-5 subtitle-2"
                              :color="chipShiftColor"
                              v-bind="attrs"
                              v-on="on"
                              >{{ transRecordFilter.ShiftDesc }}</v-chip
                            >
                          </div>
                        </div>
                      </template>
                      <v-list>
                        <v-list-item
                          v-for="(s, i) in shifts"
                          :key="i"
                          @click="selectShift(s)"
                        >
                          {{ s.ShiftDesc }}
                        </v-list-item>
                        <v-list-item @click="selectShift(null)">
                          ALL
                        </v-list-item>
                      </v-list>
                    </v-menu>
                  </div>
                </v-col>
                <v-col cols="12">
                  <AutocompleteMaterial
                    v-model="transRecordFilter.RawMaterialId"
                    :return-object="false"
                    :is-multiple="true"
                  />
                </v-col>
                <v-col cols="12">
                  <AutocompleteBinLocation
                    v-model="transRecordFilter.BinLocationId"
                    :return-object="false"
                  />
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="transRecordFilter.PalletNum"
                    label="Pallet Number"
                    placeholder=" "
                  />
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="transRecordFilter.BinNum"
                    label="Bin
              Number"
                  />
                </v-col>
              </v-row>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { enums } from "@/helpers/constants";
import shiftService from "@/services/shiftService.js";
export default {
  name: "TransRecordListFilter",
  props: {
    value: {
      type: Boolean,
      default: false
    },
    transRecordFilter: {
      type: Object,
      default: () => {}
    },
    applyFunc: {
      type: Function,
      default: () => {}
    },
    filtered: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      dTInboundFromMenu: false,
      dTInboundToMenu: false,
      transactionStatus: [
        enums.transactionStatus.PENDING,
        enums.transactionStatus.COMPLETED
      ],
      shifts: [],
      chipShiftColor: ""
    };
  },
  computed: {
    show: {
      get() {
        return this.value;
      },
      set(value) {
        console.log(value);
        this.$emit("input", value);
      }
    }
  },
  created() {
    this.init();
  },
  methods: {
    async init() {
      const response = await shiftService.list();
      this.shifts = response.data;
    },
    selectShift(s) {
      if (s == null) {
        this.transRecordFilter.ShiftId = 0;
        this.transRecordFilter.ShiftDesc = "ALL";
        this.chipShiftColor = "";
      } else {
        this.transRecordFilter.ShiftId = s.ShiftId;
        this.transRecordFilter.ShiftDesc = s.ShiftDesc;
        this.chipShiftColor = "primary";
      }
    },
    transactionStatusColor(o) {
      const r = o == this.transRecordFilter.TransactionStatus ? "primary" : "";
      console.log(r);
      return r;
    },
    apply() {
      this.show = false;
      this.applyFunc();
    },
    reset() {
      const today = new Date().toISOString().substr(0, 10);
      this.transRecordFilter.DTInboundFrom = today;
      this.transRecordFilter.DTInboundTo = today;
      this.transRecordFilter.ShiftId = 0;
      this.transRecordFilter.RawMaterialId = [];
      this.transRecordFilter.BinLocationId = [];
      this.transRecordFilter.PalletNum = "";
      this.transRecordFilter.BinNum = "";
      this.show = false;
      this.applyFunc();
    }
  }
};
</script>

<style></style>
