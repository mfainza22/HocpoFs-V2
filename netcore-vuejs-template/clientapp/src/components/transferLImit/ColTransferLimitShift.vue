<template>
  <div :class="`shift-status ${statusStyle.className}`">
    <v-edit-dialog
      large
      :return-value.sync="item.ComputedLimitKg"
      @save="save()"
    >
      <v-sheet class="shift-status__content" tile>
        <v-tooltip bottom small class="shift-status__tooltip">
          <template v-slot:activator="{ on }">
            <v-icon
              class="shift-status__icon"
              v-on="on"
              v-text="statusStyle.icon"
            ></v-icon>
          </template>
          <span
            class="shift-status__tooltip__content"
            v-text="statusStyle.tooltipMsg"
          ></span>
        </v-tooltip>
        <v-tooltip bottom>
          <template v-slot:activator="{ on }">
            <v-icon
              v-show="isAdjusted(item.AdjRemarks)"
              class="shift-status__adjusted"
              small
              v-on="on"
              >mdi-adjust</v-icon
            >
          </template>
          <span>Adjusted</span>
        </v-tooltip>
        <span class="shift-status__limit">{{
          item.ComputedLimitKg | toCommaNumeral
        }}</span>
      </v-sheet>
      <template v-slot:input>
        <v-text-field
          v-model.number="item.ComputedLimitKg"
          single-line
          autofocus
          type="number"
        ></v-text-field>
      </template>
    </v-edit-dialog>
    <v-btn
      icon
      class="shift-status__more"
      :to="{
        name: 'TransferLimitAdjs',
        params: {
          effectiveDate: item.EffectiveDate,
          rawMaterialId: item.RawMaterialId,
          shiftId: item.ShiftId
        }
      }"
    >
      <v-icon>mdi-dots-vertical</v-icon>
    </v-btn>
  </div>
</template>

<script>
import filters from "@/helpers/filters.js";

const statusStyleOptions = [
  {
    className: "",
    icon: "",
    tooltipMsg: ""
  },
  {
    className: "shift-status--warning",
    icon: "mdi-exclamation-thick",
    tooltipMsg: "Warning"
  },
  {
    className: "shift-status--invalid",
    icon: "mdi-alert",
    tooltipMsg: "Limit reached"
  },
  {
    className: "",
    icon: "",
    tooltipMsg: ""
  }
];

export default {
  name: "ColTransferLimitShift",
  filters: {
    ...filters
  },
  props: {
    item: {
      type: Object,
      default: () => {}
    },
    saveCallback: {
      type: Function,
      default: () => {}
    }
  },
  data() {
    return {};
  },
  computed: {
    statusStyle() {
      return statusStyleOptions[this.item.LimitStatus];
    }
  },
  methods: {
    isAdjusted() {
      return this.item.AdjRemarks != null;
    },
    save() {
      this.saveCallback(this.item);
    }
  }
};
</script>

<style scoped lang="scss">
$warningBg: #d4e157;
$invalidBg: #ff9100;

.shift-status {
  padding: 0;
  padding-left: 12px;
  display: flex;
  flex-basis: 1;
  align-items: center;
  min-height: 48px;
}

.shift-status {
  .v-small-dialog__activator {
    width: 100%;
  }
  .v-small-dialog__activator__content {
    display: flex;
    height: 100%;
  }
}

.shift-status__content {
  padding: 8px;
  flex: 1;
  position: relative;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}

.shift-status__adjusted {
  padding-left: 6px;
  color: #bdbdbd !important;
}

.shift-status__limit {
  flex-grow: 2;
  text-align: right;
}

.shift-status__more i {
  color: #cecdcd !important;
}

.shift-status--warning {
  background: $warningBg;

  .shift-status__content,
  .shift-status__limit {
    background-color: $warningBg !important;
  }

  .shift-status__icon {
    color: darken($warningBg, 20%);
  }

  .shift-status__adjusted {
    color: darken($warningBg, 30%) !important;
  }

  .shift-status__more i {
    color: darken($warningBg, 15%) !important;
  }
}

.shift-status--invalid {
  background-color: $invalidBg;
  .shift-status__content,
  .shift-status__limit {
    background-color: $invalidBg !important;
  }

  .shift-status__icon {
    color: darken($invalidBg, 20%);
  }

  .shift-status__adjusted {
    color: darken($invalidBg, 30%) !important;
  }

  .shift-status__more i {
    color: darken($invalidBg, 15%) !important;
  }
}
</style>
