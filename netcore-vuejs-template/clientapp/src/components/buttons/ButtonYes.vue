<template>
  <BaseButton
    color="primary"
    label="YES"
    icon="mdi-check"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonOk",
  inheritAttrs: false
};
</script>

<style></style>
