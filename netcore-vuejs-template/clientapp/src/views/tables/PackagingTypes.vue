<template>
  <div class="pa-10">
    <v-sheet class="elevation-1">
      <v-data-table
        v-model="checkedRows"
        item-key="PackagingTypeId"
        :headers="tblHeaders"
        :footer-props="{
          itemsPerPageOptions: [100, 200, 300, -1]
        }"
        :items="items"
        :search="search"
        show-select
      >
        <template v-slot:top>
          <v-toolbar flat>
            <ButtonDelete @click="confirmDelete()" />
            <v-spacer></v-spacer>
            <ButtonAdd :to="{ name: 'PackagingTypeCreate' }" />
            <v-text-field
              v-model="search"
              label="Search by Name"
              style="max-width:320px"
              color="grey"
              outlined
              dense
              hide-details
              prepend-icon="mdi-search"
              :show-select="true"
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:[`item.EmptyPackageWt`]="{ item }">
          {{ item.EmptyPackageWt }} Kg
        </template>
        <template v-slot:[`item.PackagingTypeId`]="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{
              name: 'PackagingTypeUpdate',
              params: { id: item.PackagingTypeId }
            }"
          >
            <v-icon color="success">mdi-pencil</v-icon>
          </v-btn>
        </template>
      </v-data-table>
    </v-sheet>
    <router-view></router-view>
    <MessageBox ref="messagebox" />
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import { mapState, mapActions } from "vuex";

export default {
  name: "PackagingTypes",
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Packaging Types",
      checkedRows: [],
      search: "",
      tblHeaders: [
        {
          text: "Description",
          value: "PackagingTypeDesc",
          sortable: true,
          align: "left"
        },
        {
          text: "Empty Pack. Wt",
          value: "EmptyPackageWt",
          sortable: true,
          align: "center"
        },
        {
          text: "action",
          value: "PackagingTypeId",
          align: "right"
        }
      ]
    };
  },
  computed: {
    ...mapState("packagingType", {
      items: state => state.items
    })
  },
  async created() {
    await this.init();
  },
  methods: {
    ...mapActions("packagingType", {
      list: "listPackagingType",
      delete: "deletePackagingTypes"
    }),
    async init() {
      await this.list();
      this.checkedRows = [];
    },
    async confirmDelete() {
      if (this.checkedRows.length == 0) return;

      const msgBoxOpts = {
        title: "Delete Confirmation",
        content: `Do you want to delete selected items(${this.checkedRows.length})`,
        buttons: "deleteCancel"
      };

      this.$refs.messagebox.open(msgBoxOpts, async () => {
        await this.delete(this.checkedRows);
        this.checkedRows = [];
      });
    }
  },
  beforeRouteUpdate(to, from, next) {
    /**
     * use the line of code below of no vuex
     *   if (from.meta.cancelled === false) this.init();
     */
    next();
  }
};
</script>

<style></style>
