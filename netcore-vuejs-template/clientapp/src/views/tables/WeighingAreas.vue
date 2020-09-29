<template>
  <div class="pa-10">
    <v-sheet class="elevation-1">
      <v-data-table
        v-model="checkedRows"
        item-key="WeighingAreaId"
        :headers="tblHeaders"
        :items="items"
        :search="search"
        :footer-props="{
          itemsPerPageOptions: [100, 200, 300, -1]
        }"
        show-select
      >
        <template v-slot:top>
          <v-toolbar flat>
            <ButtonDelete @click="confirmDelete()" />
            <v-spacer></v-spacer>
            <ButtonAdd :to="{ name: 'WeighingAreaCreate' }" />
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
        <template v-slot:[`item.WeighingAreaId`]="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{
              name: 'WeighingAreaUpdate',
              params: { id: item.WeighingAreaId }
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
  name: "WeighingAreas",
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Weighing Areas",
      checkedRows: [],
      search: "",
      tblHeaders: [
        {
          text: "Code",
          value: "AreaCode",
          sortable: true,
          align: "left"
        },
        {
          text: "Description",
          value: "AreaDesc",
          sortable: true,
          align: "left"
        },
        {
          text: "",
          value: "WeighingAreaId",
          align: "right"
        }
      ]
    };
  },
  computed: {
    ...mapState("weighingArea", {
      items: state => state.items
    })
  },
  async created() {
    await this.init();
  },
  methods: {
    ...mapActions("weighingArea", {
      list: "listWeighingArea",
      delete: "deleteWeighingArea"
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
