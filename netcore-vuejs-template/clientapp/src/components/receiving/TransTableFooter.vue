<template>
  <div>
    <v-divider></v-divider>
    <div class="details-footer d-flex">
      <div>
        <span class="body-1 font-weight-light mr-4">No of Items:</span>
        <span v-text="totItem"></span>
      </div>
      <v-spacer></v-spacer>
      <span class="mr-4 font-weight-light body-1">Total Quantity</span>
      <div
        style="border: 1px solid #e0e0e0; width:100px; text-align: center"
        class="details-qty px-6 py-1"
        v-text="totQty"
      ></div>
      <div class="last-col-spacer"></div>
    </div>
    <v-divider></v-divider>
  </div>
</template>

<script>
export default {
  props: {
    props: {
      type: Object,
      default: () => {}
    },
    totItem: {
      type: [String, Number],
      default: 0
    },
    totQty: {
      type: [String, Number],
      default: 0
    }
  }
};
</script>

<style lang="scss">
.details-footer {
  padding-right: 100px;
  align-items: center;

  > div {
    margin: 10px 5px;
  }
}
</style>
