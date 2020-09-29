<template>
  <v-chip :class="`mr-4 ${statusStyle.className}`" :active="hasStatus">
    <v-icon>{{ statusStyle.icon }}</v-icon>
    {{ statusStyle.tooltipMsg }}
  </v-chip>
</template>

<script>
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
  }
];

export default {
  name: "ChipTransferLimitStatus",
  props: {
    status: {
      type: Object,
      default: () => {}
    }
  },
  computed: {
    statusStyle() {
      return statusStyleOptions[this.status.LimitStatus];
    },
    hasStatus() {
      if (this.statusStyle == null) return false;
      const h = this.statusStyle.className != "";
      return h;
    }
  },
  created() {}
};
</script>

<style scoped lang="scss">
$warningBg: #d4e157;
$invalidBg: #ff9100;
.v-chip.shift-status--warning {
  background: $warningBg;
  color: darken($warningBg, 20%);

  .v-icon {
    color: darken($warningBg, 20%);
  }
}

.shift-status--invalid {
  background-color: $invalidBg !important;

  color: darken($invalidBg, 20%);

  .v-icon {
    color: darken($invalidBg, 20%);
  }
}
</style>
