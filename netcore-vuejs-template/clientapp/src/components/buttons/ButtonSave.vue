<template>
  <BaseButton
    color="primary"
    label="SAVE"
    icon="mdi-content-save"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonSave",
  inheritAttrs: false
};
</script>

<style></style>
