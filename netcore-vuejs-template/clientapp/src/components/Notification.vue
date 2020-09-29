<template>
  <v-snackbar
    v-model="notification.visible"
    :color="notification.type"
    top
    right
  >
    <v-icon
      v-show="notification.icon !== undefined"
      :color="iconColor"
      small
      left
    >
      {{ notification.icon }}
    </v-icon>
    <span v-html="notification.content"></span>
    <v-btn
      v-if="notification.showClose"
      flat
      color="primary"
      @click.native="value = false"
    ></v-btn>
  </v-snackbar>
</template>
<script>
import { mapState } from "vuex";
export default {
  data() {
    return {
      visible: false
    };
  },
  computed: {
    ...mapState("notification", {
      notification: state => state.notification
    }),
    iconColor() {
      switch (this.notification.color) {
        default:
          return "white";
      }
    }
  },
  watch: {},
  methods: {
    getContent() {
      return `<span class="green--text lighten-1">Added</span>`;
    }
  }
};
</script>

<style></style>
