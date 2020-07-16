<template>
  <div :class="baseClass">
    <v-btn
      :tile="tile"
      :icon="isIcon"
      :color="color"
      v-bind="$attrs"
      v-on="$listeners"
    >
      <template v-if="hasDefaultSlot">
        <slot />
      </template>
      <template v-else>
        <v-icon v-show="icon !== ''" :left="$vuetify.breakpoint.smAndUp">
          {{ icon }}
        </v-icon>
        <span v-show="$vuetify.breakpoint.smAndUp">{{ label }}</span>
        <slot />
      </template>
    </v-btn>
  </div>
</template>

<script>
export default {
  name: "BaseButton",
  inheritAttrs: false,
  props: {
    tile: {
      type: Boolean,
      default: true
    },
    label: {
      type: String,
      default: "Button"
    },
    icon: {
      type: String,
      default: ""
    },
    color: {
      type: String,
      default: ""
    },
    baseClass: {
      type: String,
      default: ""
    },
    isIcon: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    hasDefaultSlot() {
      return !!this.$slots.default;
    }
  }
};
</script>

<style></style>
