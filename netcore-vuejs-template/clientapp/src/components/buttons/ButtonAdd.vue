<template>
  <BaseButton
    :label="label"
    color="success"
    icon="mdi-plus"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonAdd",
  inheritAttrs: false,
  props: {
    label: {
      type: String,
      default: "ADD"
    }
  }
};
</script>

<style></style>
