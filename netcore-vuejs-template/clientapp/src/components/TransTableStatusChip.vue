<template>
  <v-chip
    class="status-chip"
    align="center"
    elevation-5
    small
    :color="statusBgColor"
    :text-color="statusTextColor"
  >
    <span>{{ statusText }}</span>
  </v-chip>
</template>

<script>
export default {
  name: "TransTableStatusChip",
  props: {
    status: {
      type: String,
      default: ""
    }
  },
  computed: {
    statusBgColor() {
      switch (status) {
        case "posted":
          return "primary";
        default:
          return "orange";
      }
    },
    statusTextColor() {
      switch (status) {
        case "posted":
          return "white";
        default:
          return "white";
      }
    },
    statusText() {
      switch (status) {
        case "posted":
          return status.toUpperCase();
        default:
          return "PENDING";
      }
    }
  }
};
</script>

<style lang="scss">
.status-chip.v-chip.v-size--x-small {
  height: 20px;
}
</style>
