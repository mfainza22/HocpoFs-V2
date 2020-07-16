<template>
  <div v-show="!componentLoading">
    <v-autocomplete
      ref="ac"
      v-model="value"
      style="border-radius:0"
      tile
      :items="items"
      :v-bind="$attrs"
      :v-on="$listeners"
      :search-input.sync="searchText"
      :filter="customFilter"
      item-text="description"
      dense
      filled
      solo
      hide-details
      clearable
      max-width="100px"
      :return-object="true"
      clear-on-select
      @input="updateValue()"
    >
      <template v-slot:no-data>
        <v-list-item>
          <v-list-item-title>
            <ButtonAdd
              small
              text
              label="Click to add new product"
              :to="{ name: 'ReceivingProductAdd' }"
            />
          </v-list-item-title>
        </v-list-item>
      </template>
      <template v-slot:item="{ item }">
        <ProductListItem :product="item" />
      </template>
    </v-autocomplete>
  </div>
</template>

<script>
import productService from "@/services/productService.js";

export default {
  name: "AutoCompleteProduct",
  data() {
    return {
      value: null,
      isLoading: false,
      componentLoading: true,
      searchText: "",
      items: []
    };
  },
  computed: {
    hasItemSlot() {
      return !!this.$scopedSlots.item;
    },
    hasSelectionSlot() {
      return !!this.$scopedSlots.selection;
    }
  },
  beforeMount() {},
  async created() {
    try {
      const response = await productService.list();
      this.items = response.data;
    } catch (error) {
      console.log(error);
    } finally {
      this.componentLoading = false;
    }
  },
  methods: {
    updateValue: function() {
      if (this.value) {
        this.$emit("input", this.value);
        this.$refs.ac.setValue("");
      }
    },
    customFilter(item, queryText) {
      const fieldOne = item.description.toLowerCase();
      const fieldTwo = item.serialCode.toLowerCase();
      const searchText = queryText.toLowerCase();
      return (
        fieldOne.indexOf(searchText) > -1 || fieldTwo.indexOf(searchText) > -1
      );
    }
  }
};
</script>

<style></style>
