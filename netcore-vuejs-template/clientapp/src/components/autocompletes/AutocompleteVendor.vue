<template>
  <div v-show="!componentLoading">
    <v-autocomplete
      v-model="selected"
      dense
      label="Vendor"
      outlined=""
      :items="items"
      v-bind="$attrs"
      placeholder=" "
      :search-input.sync="searchText"
      item-value="vendorName"
      item-text="vendorName"
      hide-details
      clearable
      max-width="100px"
      :return-object="true"
      v-on="$listeners"
      @input="updateValue()"
    >
      <template v-slot:no-data>
        <v-list-item>
          <v-list-item-title>
            <ButtonAdd
              small
              text
              label="Click to add new vendor"
              @click="addVendor"
            />
          </v-list-item-title>
        </v-list-item>
      </template>
      <!-- <template v-slot:item="{ item }">
          {{ item.vendorName }}
        </template> -->
    </v-autocomplete>
  </div>
</template>

<script>
import vendorService from "@/services/vendorService.js";

export default {
  inheritAttrs: false,
  props: {
    value: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      items: [],
      options: {
        label: "Vendor",
        itemValue: "vendorName",
        itemText: "vendorName"
      },
      selected: {},
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
      const response = await vendorService.list();
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
        const response = await vendorService.create(obj);
        this.items.push(response.data);
        return response;
      } catch (error) {
        console.log(error);
        return error;
      }
    },
    updateValue: function() {
      this.$emit("input", this.selected);
    }
  }
};
</script>

<style></style>
