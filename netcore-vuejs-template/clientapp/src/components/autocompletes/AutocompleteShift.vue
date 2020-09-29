<template>
  <div v-show="isLoaded">
    <v-autocomplete
      v-model="selected"
      dense
      label="Vendor"
      :items="items"
      v-bind="$attrs"
      placeholder=" "
      :search-input.sync="searchText"
      item-value="ShiftId"
      item-text="ShiftDesc"
      hide-details
      chips
      clearable
      small-chips
      multiple
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
              label="Click to add new Shift"
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
import shiftService from "@/services/shiftService.js";

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
        label: "Shift",
        itemValue: "ShiftId",
        itemText: "ShiftId"
      },
      selected: [],
      searchText: "",
      isLoaded: false
    };
  },
  computed: {},
  watch: {
    value() {
      this.selected = this.value;
    }
  },
  async created() {
    this.isLoaded = true;
    try {
      const response = await shiftService.list();
      this.items = response.data;
      return response;
    } catch (error) {
      console.log(error);
      return error;
    } finally {
      this.isLoaded = true;
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
      this.$emit("input", this.selected);
    }
  }
};
</script>

<style></style>
