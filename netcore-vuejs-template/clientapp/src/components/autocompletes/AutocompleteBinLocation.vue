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
import { mapState, mapActions } from "vuex";

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
      options: {
        label: "Bin Location",
        itemValue: "BinLocationId",
        itemText: "BinLocDesc",
        maxWidth: "200px",
        addLabel: "Click to add new bin location"
      },
      selected: [],
      searchText: "",
      isLoaded: false
    };
  },
  computed: {
    ...mapState("binLocation", {
      items: state => state.items
    })
  },
  watch: {
    value() {
      this.selected = this.value;
    },
    searchText() {
      if (this.searchText == "") {
        this.selected = null;
        this.updateValue();
      }
    }
  },
  async created() {
    this.selected = this.value;
    try {
      await this.list();
    } finally {
      this.isLoaded = true;
    }
  },
  methods: {
    ...mapActions("binLocation", {
      list: "listBinLocation",
      create: "createBinLocation"
    }),
    async add() {
      const obj = {
        BinLocDesc: this.searchText
      };
      await this.create(obj);
    },
    updateValue: function() {
      this.$emit("input", this.selected);
    }
  }
};
</script>

<style></style>
