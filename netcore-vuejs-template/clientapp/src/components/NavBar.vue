<template>
  <div>
    <v-navigation-drawer v-model="drawer" app dark>
      <NavList />
    </v-navigation-drawer>
    <v-app-bar
      app
      clipped
      color="white"
      class="elevation-custom"
      style="border:none"
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>{{ pageTitle }}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-avatar
            to="/"
            color="white"
            style="border: 1px solid white"
            class=""
            v-on="on"
          >
            <v-img src="/avatars/21EB0B92-5559-4B3A-90B5-E60806C07441.png">
            </v-img>
          </v-avatar>
        </template>
        <div>
          <v-list>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title>Michael Fainza</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </div>
      </v-menu>
    </v-app-bar>
  </div>
</template>

<script>
import { EventBus } from "@/main.js";

export default {
  name: "NavBar",
  data() {
    return {
      drawer: true,
      pageTitle: "PageTitle"
    };
  },
  created() {
    EventBus.$on("setPageTitle", this.onSetPageTitle);
    EventBus.$on("hideDrawer", this.hideDrawer);
  },
  methods: {
    onSetPageTitle(val) {
      this.pageTitle = val;
    },
    hideDrawer() {
      this.drawer = false;
    }
  }
};
</script>

<style lang="scss">
.app-bar-brand {
  height: 64px;
  display: flex;
  justify-content: center;
  overflow-y: hidden;
  border-radius: 0;
}
</style>
