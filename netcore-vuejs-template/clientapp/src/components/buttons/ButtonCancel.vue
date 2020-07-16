<template>
  <BaseButton
    label="CANCEL"
    icon="mdi-cancel"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonCancel",
  inheritAttrs: false
};
</script>

<style></style>
