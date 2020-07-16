<template>
  <BaseButton
    color="error"
    label="DELETE"
    icon="mdi-delete"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonDelete",
  inheritAttrs: false
};
</script>

<style></style>
