<template>
  <v-snackbar
    v-if="limitStatus != null"
    v-model="visible"
    :timeout="0"
    tile
    top
    center
    :class="`limitstatus limitstatus--${status.className} mb-2 mr-8`"
    :color="status.bgColor"
    width="300"
    multi-line
  >
    <div class="limitstatus__content">
      <div class="caption">
        <div class="limitstatus__text font-weight-bold">
          {{ formattedEffectiveDate }} {{ limitStatus.ShiftDesc }}
        </div>
        <div class="limitstatus__text" style="height:28px; padding-top:4px">
          {{ limitStatus.RawMaterialDesc }}
        </div>
      </div>
      <div class="text-center">
        <span class="limitstatus__text caption">Limit Status</span>
        <v-progress-linear
          :value="progressBarValue"
          striped
          height="25"
          :color="status.barBgColor"
          class="mb-1"
          tile
          rounded
        >
          <template v-slot="{}">
            <span class="limitstatus__text font-weight-black">
              {{ limitStatus.TransferredKg }} of
              {{ limitStatus.ComputedLimitKg }}
            </span>
          </template>
        </v-progress-linear>
      </div>
      <h2 class="limitstatus__text">{{ status.message }}</h2>
    </div>
  </v-snackbar>
</template>

<script>
var moment = require("moment");

export default {
  name: "TransLimitStatus",
  props: {
    limitStatus: {
      type: Object,
      default: () => {
        return {
          EffectiveDate: new Date().toISOString().substring(0, 9),
          ComputedLimitKg: 32000,
          CurrentLimitKg: 32000,
          LimitStatus: 0,
          LimitStatusMsg: "N/A",
          LimitWarningKg: 25600,
          RawMaterialDesc: "",
          ShiftId: 0,
          ShiftDesc: "",
          TransferLimitId: 38720,
          TransferredKg: 0
        };
      }
    }
  },
  data() {
    return {
      visible: true,
      limitStatusOptions: [
        {
          className: "normal",
          message: "NORMAL",
          bgColor: "blue lighten-1",
          barBgColor: "blue lighten-4"
        },
        {
          className: "warning",
          message: "WARNING",
          bgColor: "yellow lighten-1",
          barBgColor: "yellow lighten-4"
        },
        {
          className: "invalid",
          message: "INVALID",
          bgColor: "red lighten-1",
          barBgColor: "red lighten-3"
        },
        {
          className: "nolimit",
          message: "NO LIMIT SET",
          bgColor: "",
          barBgColor: ""
        }
      ]
    };
  },
  computed: {
    status() {
      let s = 3;
      if (Object.keys(this.limitStatus).length > 0)
        s = this.limitStatus.LimitStatus;
      return this.limitStatusOptions[s];
    },
    progressBarValue() {
      if (
        this.limitStatus.TransferredKg == 0 ||
        this.limitStatus.ComputedLimitKg == 0
      )
        return 0;
      return (
        (this.limitStatus.TransferredKg / this.limitStatus.ComputedLimitKg) *
        100
      );
    },
    formattedEffectiveDate() {
      return moment(this.limitStatus.EffectiveDate).format("MMM-DD-y");
    }
  },
  watch: {}
};
</script>

<style lang="scss">
.limitstatus__content {
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

.limitstatus__content > :nth-child(1) {
  min-width: 150px;
}

.limitstatus__content > :nth-child(2) {
  min-width: 200px;
}

h2.limitstatus__text {
  margin-left: 1rem;
}
.limitstatus--normal {
  .limitstatus__text {
    color: #fff;
  }
}

.limitstatus--warning {
  .limitstatus__text {
    color: #ff5722;
  }
}

.limitstatus--invalid {
  .limitstatus__text {
    color: #fff;
  }
}

.limitstatus--nolimit {
  .limitstatus__text {
    color: #fff;
  }
}
</style>
