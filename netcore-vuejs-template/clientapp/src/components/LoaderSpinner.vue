<template>
  <Loading :active.sync="isLoading" :is-full-page="true"></Loading>
</template>

<script>
import Loading from "vue-loading-overlay";
import "vue-loading-overlay/dist/vue-loading.css";
import { mapState, mapActions } from "vuex";

export default {
  name: "LoaderSpinner",
  components: {
    Loading
  },
  data: () => ({}),
  computed: {
    ...mapState("loaderSpinner", {
      isLoading: state => state.isLoading
    })
  },
  methods: {
    ...mapActions("loaderSpinner", ["show", "hide"])
  }
};
</script>

<style></style>
