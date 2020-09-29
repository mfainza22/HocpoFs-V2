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
      clear-icon="mdi-close"
      clearable
      placeholder=" "
      v-bind="$attrs"
      v-on="listeners"
      @click:clear="onClear"
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
        label: "Packaging Type",
        itemValue: "PackagingTypeId",
        itemText: "PackagingTypeDesc",
        maxWidth: "200px",
        addLabel: "Click to add new packaging type"
      },
      selected: [],
      searchText: "",
      isLoaded: false
    };
  },
  computed: {
    listeners() {
      return {
        ...this.$listeners
      };
    },
    ...mapState("packagingType", {
      items: state => state.items
    })
  },
  watch: {
    value() {
      this.selected = this.value;
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
    ...mapActions("packagingType", {
      list: "listPackagingType",
      create: "createPackagingType"
    }),
    async add() {
      const obj = {
        packagingTypeDesc: this.searchText
      };
      await this.create(obj);
    },
    onClear() {},
    updateValue: function() {
      this.$emit("input", this.selected);
    }
  }
};
</script>

<style></style>
