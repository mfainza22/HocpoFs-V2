<template>
  <div class="pa-10">
    <v-sheet class="elevation-1">
      <v-data-table
        v-model="checkedRows"
        item-key="id"
        :headers="tblHeaders"
        :items="items"
        :search="search"
        show-select
      >
        <template v-slot:top>
          <v-toolbar flat>
            <ButtonDelete @click="remove()" />
            <v-spacer></v-spacer>
            <ButtonAdd :to="{ name: 'WarehouseCreate' }" />
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
        <template v-slot:item.id="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{ name: 'WarehouseUpdate', params: { id: item.id } }"
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
import warehouseService from "@/services/warehouseService.js";

export default {
  name: "Warehouses",
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Warehouses",
      checkedRows: [],
      search: "",
      items: [],
      tblHeaders: [
        {
          text: "Code",
          value: "WarehouseCode",
          sortable: true,
          align: "left"
        },
        {
          text: "Name",
          value: "WarehouseName",
          sortable: true,
          align: "left"
        },
        {
          text: "action",
          value: "id",
          align: "right"
        }
      ]
    };
  },
  watch: {},
  async created() {
    await this.init();
  },
  methods: {
    async init() {
      try {
        const response = await warehouseService.list();
        this.items = response.data;
        this.checkedRows = [];
      } catch (error) {
        console.log(error);
      }
    },
    async remove() {
      if (this.checkedRows.length == 0) return;

      const msgBoxOpts = {
        title: "Delete Confirmation",
        content: `Do you want to delete selected items(${this.checkedRows.length})`,
        buttons: "deleteCancel"
      };
      const noti = {
        visible: true
      };

      this.$refs.messagebox.open(msgBoxOpts, async () => {
        try {
          let params = this.checkedRows.map(a => a.id);
          await warehouseService.delete(params);

          noti.type = "success";
          noti.content = `${params.length} ${
            params.length == 1 ? "record" : "records"
          } deleted successfully`;

          this.init();
        } catch (error) {
          noti.type = "error";
          this.internalErrorMsg = error;
          noti.content = error;
        } finally {
          this.showNotification(noti);
        }
      });
    }
  },
  beforeRouteUpdate(to, from, next) {
    if (!from.meta.cancelled == true) this.init();
    next();
  }
};
</script>

<style></style>