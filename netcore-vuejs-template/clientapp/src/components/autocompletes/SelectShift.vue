<template>
  <div v-show="!componentLoading">
    <v-select
      v-model="selected"
      dense
      label="Shift"
      :items="items"
      v-bind="$attrs"
      placeholder=" "
      item-value="ShiftId"
      item-text="ShiftDesc"
      max-width="100px"
      v-on="$listeners"
      @input="updateValue()"
    >
    </v-select>
  </div>
</template>

<script>
import shiftService from "@/services/shiftService.js";

export default {
  inheritAttrs: false,
  props: {
    value: {
      type: Number || String,
      default: () => {}
    }
  },
  data() {
    return {
      items: [],
      options: {
        label: "Shift",
        itemValue: "ShiftId",
        itemText: "ShiftId"
      },
      selected: 0,
      searchText: "",
      componentLoading: true
    };
  },
  computed: {},
  watch: {
    value() {
      this.selected = this.value;
    }
  },
  async created() {
    this.componentLoading = true;
    try {
      const response = await shiftService.list();
      this.items = response.data;
      return response;
    } catch (error) {
      console.log(error);
      return error;
    } finally {
      this.componentLoading = false;
    }
  },
  methods: {
    async addVendor() {
      const obj = {
        vendorName: this.searchText
      };

      try {
        const response = await shiftService.create(obj);
        this.items.push(response.data);
        return response;
      } catch (error) {
        console.log(error);
        return error;
      }
    },
    updateValue: function() {
      console.log(this.selected);
      this.$emit("input", this.selected);
    }
  }
};
</script>

<style></style>
