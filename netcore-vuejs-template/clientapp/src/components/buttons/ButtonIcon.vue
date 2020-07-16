<template>
  <v-btn class="button-icon" depressed small v-bind="$attrs" v-on="$listeners">
    <v-icon small :color="iconColor">{{ btnIcon }}</v-icon>
  </v-btn>
</template>

<script>
export default {
  name: "ButtonIcon",
  inheritAttrs: false,
  props: {
    label: {
      type: String,
      default: ""
    },
    btnIcon: {
      type: String,
      default: "mdi-plus"
    },
    iconColor: {
      type: String,
      default: ""
    }
  }
};
</script>

<style lang="scss">
.v-btn:not(.v-btn--round).v-size--small.button-icon {
  min-width: 20px;
  padding: 4px;
}
</style>
