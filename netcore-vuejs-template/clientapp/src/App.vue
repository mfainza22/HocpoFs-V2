<template>
  <v-app>
    <NavBar ref="navbar" />
    <Notification />
    <v-content>
      <keep-alive>
        <router-view></router-view>
      </keep-alive>
    </v-content>
  </v-app>
</template>

<script>
var moment = require("moment");

export default {
  name: "App",
  data: () => ({
    //
  }),
  created() {
    var mm = moment.tz("America/Los_Angeles").format();

    console.log(mm);
  },
  mounted() {},
  methods: {}
};
</script>
<style scoped></style>
