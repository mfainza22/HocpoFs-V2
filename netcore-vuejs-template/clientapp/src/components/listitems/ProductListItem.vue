<template>
  <v-list-item three-line style="">
    <v-list-item-content>
      <v-list-item-title v-text="product.description"></v-list-item-title>
      <v-list-item-subtitle v-text="product.category"></v-list-item-subtitle>
      <div class=" mt-2 d-flex justify-space-between">
        <span class="overline primary--text">#{{ product.serialCode }}</span>
        <span class="overline pr-4">Available: {{ product.onStock }}</span>
      </div>
    </v-list-item-content>
    <v-avatar tile size="70" color="white">
      <v-img :src="imgUrl"></v-img>
    </v-avatar>
  </v-list-item>
</template>

<script>
export default {
  props: {
    product: {
      type: Object,
      default: () => {}
    }
  },
  computed: {
    imgUrl() {
      let img = "";

      if (!this.product) {
        img = "no-img.png";
      } else if (!this.product.img) {
        img = "no-img.png";
      } else {
        img = this.product.img;
      }

      return `/products/${img}`;
    }
  }
};
</script>

<style></style>
