<template>
  <v-app>
    <LoaderSpinner ref="loaderSpinner" />
    <NavBar ref="navbar" />
    <Notification />
    <v-content>
      <!-- <transition name="slide" mode="out-in"> -->
      <router-view></router-view>
      <!-- </transition> -->
    </v-content>
  </v-app>
</template>

<script>
// var moment = require("moment");
// var mm = moment.tz("America/Los_Angeles").format();
// console.log(mm);

export default {
  name: "App",
  watch: {},
  created() {},
  mounted() {},
  methods: {}
};
</script>
<style></style>
