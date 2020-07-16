<template>
  <div class="pa-10">
    <ReceivingListTable />
    <router-view />
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
export default {
  mixins: [navbarMixins],
  data() {
    return {
      pageTitle: "Receiving"
    };
  }
};
</script>

<style></style>
