<template>
  <div v-show="isLoaded">
    <v-autocomplete
      v-model="selected"
      :search-input.sync="searchText"
      :label="options.label"
      :items="items"
      :item-value="options.itemValue"
      :item-text="options.itemText"
      :chips="isMultiple"
      :small-chips="isMultiple"
      :multiple="isMultiple"
      clearable
      dense
      placeholder=" "
      v-bind="$attrs"
      v-on="$listeners"
    >
      <template v-slot:no-data>
        <v-list-item>
          <v-list-item-title>
            <ButtonAdd small text :label="options.addLabel" @click="add" />
          </v-list-item-title>
        </v-list-item>
      </template>
    </v-autocomplete>
  </div>
</template>

<script>
import rawMaterialService from "@/services/rawMaterialService.js";

export default {
  inheritAttrs: false,
  props: {
    value: {
      type: Object || Array,
      default: []
    },
    returnObject: {
      type: Boolean,
      default: () => {}
    }
  },
  data() {
    return {
      items: [],
      options: {
        label: "Material",
        itemValue: "RawMaterialId",
        itemText: "RawMaterialDesc",
        maxWidth: "200px",
        addLabel: "Click to add new Material"
      },
      selected: [],
      searchText: "",
      isLoaded: true
    };
  },
  computed: {},
  watch: {
    value() {
      this.selected = this.value;
    }
  },
  async created() {
    try {
      const response = await rawMaterialService.list();
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
    async add() {
      const obj = {
        vendorName: this.searchText
      };

      try {
        const response = await rawMaterialService.create(obj);
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
