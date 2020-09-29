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
      type: [Number, Object, Array],
      default: () => []
    },
    returnObject: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      items: [],
      options: {
        label: "Material",
        itemValue: "RawMaterialId",
        itemText: "RawMaterialDesc",
        addLabel: "Click to add new Material"
      },
      selected: [],
      searchText: "",
      isLoaded: false
    };
  },
  watch: {
    value() {
      this.selected = this.value;
    },
    searchText() {}
  },
  async created() {
    this.isLoaded = true;
    this.selected = this.value;
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
        RawMaterialDesc: this.searchText
      };

      try {
        const response = await rawMaterialService.create(obj);
        this.items.push(response.data);
        return response;
      } catch (error) {
        console.log(error);
        return error;
      }
    }
  }
};
</script>

<style></style>
