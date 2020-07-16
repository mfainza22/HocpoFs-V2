<template>
  <div>
    <v-sheet class="app-bar-brand grey lighten-3">
      <v-img
        alt="Vuetify Logo"
        class="shrink mr-2"
        contain
        src="../assets/logo.png"
        transition="scale-transition"
        width="200"
      />
    </v-sheet>
    <div
      v-bar="{ preventParentScroll: true, scrollThrottle: 30 }"
      class="vb-element"
    >
      <div>
        <template v-for="item in menuItems">
          <v-layout v-if="item.subitems.length == 0" :key="item.title">
            <v-list-item :color="menuStyle.color" link :to="item.link">
              <v-list-item-icon>
                <v-icon
                  :small="menuStyle.small"
                  class="grey--text text--lighten-1 "
                  :class="menuStyle.class"
                  >{{ item.icon }}</v-icon
                >
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title :class="`${menuStyle.class}`">{{
                  item.title
                }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-layout>
          <v-list-group
            v-else
            :key="item.title"
            :small="menuStyle.small"
            :color="menuStyle.color"
            no-action
          >
            <template v-slot:activator>
              <v-list-item-icon>
                <v-icon
                  class="grey--text text--lighten-1 "
                  :class="menuStyle.class"
                  >{{ item.icon }}</v-icon
                >
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title
                  :class="`${menuStyle.class}`"
                  v-text="item.title"
                ></v-list-item-title>
              </v-list-item-content>
            </template>
            <v-list-item
              v-for="subitem in item.subitems"
              :key="subitem.title"
              :class="menuStyle.class"
              :color="menuStyle.color"
              link
              :to="subitem.link"
            >
              <v-list-item-title
                :class="`${menuStyle.class}`"
                v-text="subitem.title"
              ></v-list-item-title>
              <v-list-item-icon>
                <v-icon :class="menuStyle.class" v-text="subitem.icon"></v-icon>
              </v-list-item-icon>
              <div v-if="hasBadge(subitem)">
                <v-badge
                  :color="subitem.badge.color"
                  overlap
                  :content="subitem.badge.value"
                  offset-y="6"
                  offset-x="26"
                ></v-badge>
              </div>
            </v-list-item>
          </v-list-group>
        </template>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "NavList",
  data: () => ({
    menuStyle: {
      color: "grey darken-2",
      small: false
    },
    menuItems: [
      {
        icon: "mdi-view-dashboard",
        title: "Transaction",
        link: "/",
        subitems: []
      },
      {
        icon: "mdi-database",
        title: "Tables",
        link: "/",
        subitems: [
          { icon: "", title: "Bin Locations", link: { name: "BinLocations" } },
          {
            icon: "",
            title: "Packaging Types",
            link: { name: "PackagingTypes" }
          },
          { icon: "", title: "Pallets", link: { name: "Pallets" } },
          { icon: "", title: "Raw Materials", link: { name: "RawMaterials" } },
          { icon: "", title: "Forklifts", link: { name: "Forklifts" } },
          { icon: "", title: "Warehouses", link: { name: "Warehouses" } },
          { icon: "", title: "Locations", link: { name: "Locations" } },
          {
            icon: "",
            title: "Weighing Areas",
            link: { name: "WeighingAreas" }
          },
          {
            icon: "",
            title: "Shift Schedules",
            link: { name: "Shifts" }
          }
        ]
      },
      {
        icon: "mdi-database-check",
        title: "Management",
        link: "/",
        subitems: [
          { icon: "", title: "Transfer Limits", link: "/TransferLimits" }
        ]
      },
      {
        icon: "mdi-database-check",
        title: "Reports",
        link: "/",
        subitems: [{ icon: "", title: "Reports", link: "/Reports" }]
      },
      {
        icon: "mdi-folder",
        title: "Admin",
        link: "",
        subitems: [
          {
            icon: "",
            title: "UserAccounts",
            link: { name: "UserAccounts" }
          }
        ]
      },
      {
        icon: "mdi-folder",
        title: "Setup",
        link: "",
        subitems: [
          {
            icon: "",
            title: "General",
            link: { name: "SetupGeneral" }
          },
          {
            icon: "",
            title: "Client Machines",
            link: { name: "SetupClientMachines" }
          }
        ]
      }
    ]
  }),
  methods: {
    setBadge(link) {
      link.hasBadge;
    },
    hasBadge(link) {
      let r = typeof link.badge !== "undefined";
      return r;
    }
  }
};
</script>

<style lang="scss"></style>
