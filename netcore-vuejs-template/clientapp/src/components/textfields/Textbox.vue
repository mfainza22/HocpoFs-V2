<template>
  <v-text-field
    v-model="value"
    dense
    outlined
    v-bind="$attrs"
    @input="updateValue()"
    v-on="$listeners"
  ></v-text-field>
</template>

<script>
export default {
  name: "Textbox",
  inheritAttrs: false,
  data() {
    return {
      value: ""
    };
  },
  methods: {
    updateValue() {
      this.$emit("input", this.value);
    }
  }
};
</script>

<style></style>
