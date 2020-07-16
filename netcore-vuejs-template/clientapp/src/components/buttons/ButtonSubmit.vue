<template>
  <BaseButton
    color="primary"
    label="SUBMIT"
    icon="mdi-check"
    v-bind="$attrs"
    v-on="$listeners"
  />
</template>

<script>
export default {
  name: "ButtonSubmit",
  inheritAttrs: false
};
</script>

<style></style>
