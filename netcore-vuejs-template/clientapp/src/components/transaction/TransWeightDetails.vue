<template>
  <v-sheet class="weight-details">
    <div class="mb-4">
      <div class="d-flex justify-space-between">
        <label class="v-label-custom">
          Receipt Num.
        </label>
        <h2>
          {{ transRecord.ReceiptNum }}
        </h2>
      </div>
      <div class="d-flex justify-space-between">
        <label class="v-label-custom">
          Control Num.
        </label>
        <h4>
          {{ transRecord.ControlNum }}
        </h4>
      </div>
    </div>
    <template v-if="weightSourceEnabled">
      <div class="d-flex align-center mb-4">
        <v-switch
          v-model="transRecord.IsOffline"
          inset
          color="red"
          class="d-inline-block ma-0"
          hide-details
        >
        </v-switch>
        <span class="red--text text--accent-2">Enable Offline Weighing</span>
      </div>
      <div style="overflow: hidden;height:120px">
        <transition name="slide" mode="out-in">
          <v-sheet
            v-if="transRecord.IsOffline"
            :key="1"
            class="weight-details__src"
          >
            <div class="d-flex justify-space-between">
              <span class="weight-details__label">Manual Weight</span>
              <v-chip v-if="hasWeightError" color="red" dark x-small dense>
                Invalid Weight.</v-chip
              >
            </div>
            <v-text-field
              v-model.number="transRecord.OfflineWt"
              type="number"
              dark
              solo
              class="weight-details__src-offline"
              hide-details
              @input="computeWeight()"
            ></v-text-field>
            <v-sheet>
              <v-menu
                v-model="datePickerMenu"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on, attrs }">
                  <span
                    color="primary"
                    class="elevation-0 mr-2"
                    v-bind="attrs"
                    v-on="on"
                  >
                    {{ dtHelper.formattedDate }}
                    <v-icon>mdi-calendar</v-icon>
                  </span>
                </template>
                <v-date-picker v-model="dtHelper.date" no-title scrollable>
                </v-date-picker>
              </v-menu>
              <v-menu
                v-model="timePickerMenu"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px"
              >
                <template v-slot:activator="{ on, attrs }">
                  <span
                    color="primary"
                    class="elevation-0 mx-2"
                    v-bind="attrs"
                    v-on="on"
                  >
                    {{ dtHelper.formattedTime }}
                    <v-icon>mdi-clock</v-icon></span
                  >
                </template>
                <v-time-picker
                  v-model="dtHelper.time"
                  scrollable
                  ampm-in-title
                  small
                >
                </v-time-picker>
              </v-menu>
            </v-sheet>
          </v-sheet>
          <v-sheet v-if="!isOffline" :key="2" class="weight-details__src">
            <span class="weight-details__label">Weight from Device</span>
            <div dark solo class="weight-details__src-online">
              {{ transRecord.OnlineWt }}
            </div>
          </v-sheet>
        </transition>
      </div>
    </template>
    <div class="weight-details__item">
      <span class="weight-details__label"> Gross Wt </span>
      <span class="weight-details__value dark">{{
        transRecord.GrossWt | toCommaNumeral
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label"> Tare Wt </span>
      <span class="weight-details__value dark">{{
        transRecord.TareWt | toCommaNumeral
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label"> Net Wt </span>
      <span class="weight-details__value dark">{{
        transRecord.NetWt | toCommaNumeral
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label"> Actual Wt/Pack </span>
      <span class="weight-details__value">
        <v-chip :color="actualWtStatus.color" dark x-small>
          {{ actualWtStatus.label }}
        </v-chip>
        {{ transRecord.WtPerPackageActual | toFixed(2) }}</span
      >
      <span class="weight-details__unit">Kg</span>
    </div>
    <v-expand-transition>
      <v-sheet
        v-if="transRecord.WeightStatus == 'FAILED'"
        :color="actualWtStatus.color"
        dark
        class="pa-1 text-center elevation-2"
      >
        <span class="caption">
          Actual Wt Per pack should be between <br />
          {{ transRecord.MinActualWt | toFixed(2) }}~{{
            transRecord.MaxActualWt | toFixed(2)
          }}
        </span>
      </v-sheet>
    </v-expand-transition>
    <div class="weight-details__item">
      <span class="weight-details__label caption"> Declared Wt/Pack </span>
      <span class="weight-details__value">{{
        transRecord.WtPerPackage | toFixed(2)
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label"> Total Empty Pack Wt </span>
      <span class="weight-details__value">{{
        transRecord.TotalEmptyPackWt | toFixed(2)
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label"> Actual Net Wt </span>
      <span class="weight-details__value">{{
        transRecord.ActualNetWt | toCommaNumeral
      }}</span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <!-- <div class="weight-details__item">
      <span class="weight-details__label"> Tolerance</span>
      <span class="weight-details__value "> </span>
      <span class="weight-details__unit">Kg</span>
    </div>
    <div class="weight-details__item">
      <span class="weight-details__label">Tolerance Status</span>
      <v-chip :color="actualWtStatus.color" dark class="title">
        {{ actualWtStatus.label }}
      </v-chip>
    </div> -->
  </v-sheet>
</template>

<script>
import filters from "@/helpers/filters";
import { enums } from "@/helpers/constants.js";
import Vue from "vue";
import { mapState } from "vuex";
import VueSocketIO from "vue-socket.io";
import DTHelper from "@/helpers/DTHelper";
import { objectUtil } from "@/helpers/func";

Vue.use(
  new VueSocketIO({
    debug: true,
    connection: "http://localhost:3000",
    vuex: {
      actionPrefix: "SOCKET_",
      mutationPrefix: "SOCKET_"
    }
  })
);

export default {
  name: "TransWeightDetails",
  filters: {
    ...filters
  },
  props: {
    transRecord: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      connected: false,
      isOffline: false,
      dtHelper: new DTHelper(),
      datePickerMenu: false,
      timePickerMenu: false,
      weightSourceOptions: [
        {
          label: "Weight from Device"
        },
        {
          label: "Manual Weight"
        }
      ],
      actualWtStatusOptions: [
        {
          label: "NONE",
          color: "grey lighten-2"
        },
        {
          label: "PASSED",
          color: "primary"
        },
        {
          label: "FAILED",
          color: "red lighten-1"
        }
      ]
    };
  },
  sockets: {
    connect() {
      // Fired when the socket connects.
      this.isConnected = true;
      console.log("CONNECTED");
    },

    disconnect() {
      this.isConnected = false;
    },
    message(data) {
      this.onlineWt = data;
    }
  },
  computed: {
    ...mapState("transRecord", {
      transRecordValidationResult: state => state.transRecordValidationResult
    }),
    hasWeightError() {
      return objectUtil.empty(this.transRecordValidationResult);
      // if (ObjectUtil.empty(this.transRecordValidationResult)) return false;
      // const obVR = Object.keys(this.transRecordValidationResult);
      // return obVR.contains("WtInbound") || obVR.contains("WtOutbound");
    },
    weightSourceEnabled() {
      if (
        this.transRecord.TransactionProcess ==
          enums.transactionProcess.WEIGH_IN ||
        this.transRecord.TransactionProcess ==
          enums.transactionProcess.WEIGH_OUT
      ) {
        return true;
      } else return false;
    },
    weightSource() {
      if (!this.weightSourceEnabled) return this.weightSourceOptions[0];
      let wso = 0;
      if (
        this.transRecord.TransactionProcess == enums.transactionProcess.WEIGH_IN
      ) {
        wso = this.transRecord.OfflineIn ? 1 : 2;
      } else if (
        this.transRecord.TransactionProcess ==
        enums.transactionProcess.WEIGH_OUT
      ) {
        wso = this.transRecord.OfflineOut ? 1 : 2;
      }
      return this.weightSourceOptions[wso];
    },
    weightSourceValue() {
      let trec = this.transRecord;
      let wt = 0;
      wt = trec.IsOffline ? trec.OfflineWt : trec.OnlineWt;
      wt = wt == "" ? 0 : wt;
      return parseFloat(wt);
    },
    actualWtStatus() {
      let i = 0;
      let tRec = this.transRecord;

      if (tRec.TransactionProcess == enums.transactionProcess.WEIGH_IN)
        return this.actualWtStatusOptions[i];

      if (
        tRec.WtPerPackageActual >= tRec.MinActualWt &&
        tRec.WtPerPackageActual <= tRec.MaxActualWt
      ) {
        i = 1;
      } else {
        i = 2;
      }
      const r = this.actualWtStatusOptions[i];
      tRec.WeightStatus = r.label;
      return r;
    }
  },
  watch: {
    "transRecord.EmptyPackageWt": function() {
      this.computeWeight();
    },
    "transRecord.WtPerPackage": function() {
      this.computeWeight();
    },
    "transRecord.Quantity": function() {
      this.computeWeight();
    },
    "dtHelper.date": function() {
      this.transRecord.DTOfflineDate = this.dtHelper.jsDate;
    },
    "dtHelper.time": function() {
      this.transRecord.DTOfflineDate = this.dtHelper.jsDate;
    },
    "transRecord.IsOffline": function() {
      if (this.transRecord.IsOffline) {
        this.dtHelper = new DTHelper();
        this.transRecord.DTOfflineDate = this.dtHelper.jsDate;
      } else {
        this.transRecord.DTOfflineDate = null;
      }
      this.computeWeight();
    },
    onlineWt() {
      this.computeWeight();
    }
  },
  created() {
    this.computeWeight();
  },
  methods: {
    computeWeight() {
      const tRec = this.transRecord;
      let wt = this.weightSourceValue;
      if (tRec.TransactionProcess == enums.transactionProcess.WEIGH_IN) {
        tRec.WtInbound = wt;
        tRec.GrossWt = wt;
        tRec.TareWt = 0;
        tRec.NetWt = 0;
      } else if (
        tRec.TransactionProcess == enums.transactionProcess.WEIGH_OUT
      ) {
        tRec.WtOutbound = wt;
        tRec.GrossWt = tRec.WtInbound >= wt ? tRec.WtInbound : wt;
        tRec.TareWt = tRec.WtInbound <= wt ? tRec.WtInbound : wt;
        tRec.NetWt = tRec.GrossWt - tRec.TareWt;
        let wtpa = tRec.NetWt / tRec.Quantity;
        tRec.WtPerPackageActual = wtpa === Infinity ? 0 : wtpa;

        tRec.TotalEmptyPackWt = tRec.EmptyPackageWt * tRec.Quantity;
        tRec.ActualNetWt = tRec.NetWt - tRec.TotalEmptyPackWt;

        const packWt = tRec.EmptyPackageWt + tRec.WtPerPackage;
        const packwtTol = packWt * (tRec.TolActualWt * 0.01);

        tRec.MinActualWt = packWt - packwtTol;
        tRec.MaxActualWt = packWt + packwtTol;
      }
    }
  }
};
</script>

<style lang="scss">
.weight-details {
  &__src-toggle {
    display: flex;
    justify-content: space-between;
  }

  &__src-online {
    background-color: #1e1e1e;
    color: #dce775;
    padding: 13px;
    font-family: ds-digital;
    font-weight: 600;
    font-size: 250%;
    border-radius: 4px;
    text-align: right;
    margin-bottom: 8px;
    @extend .shadowed;
  }

  &__src-offline {
    margin-bottom: 8px !important;
    input {
      color: #e57373 !important;
      font-weight: 600;
      text-align: right;
      font-size: 140%;
    }
  }

  &__date {
    margin-bottom: 8px;
  }

  & > &__item {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  &__label {
    font-size: 0.75rem !important;
    font-weight: 400;
    letter-spacing: 0.0333333333em !important;
    line-height: 1.25rem;
    flex: 0.6;
  }

  &__value {
    font-size: 1rem !important;
    font-weight: 500;
    line-height: 2rem;
    letter-spacing: 0.0125em !important;
    flex: 0.6;
    text-align: right;
  }

  &__value-lg {
    font-size: 1.25rem !important;
    font-weight: 500;
    line-height: 2rem;
    letter-spacing: 0.0125em !important;
  }

  &__value.dark {
    background-color: #1e1e1e;
    color: white;
    width: 140px;
    text-align: right;
    margin-bottom: 4px;
    padding-right: 8px;
    border-radius: 4px;
    @extend .shadowed;
  }
  &__unit {
    flex: 0.1;
    text-align: right;
    align-items: center;
  }
}
</style>
