<template>
  <div class="pa-10">
    <v-sheet class="elevation-1">
      <v-data-table
        v-model="selected"
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
            <ButtonAdd :to="{ name: 'VendorCreate' }" />
            <v-text-field
              v-model="search"
              label="Search by Name"
              style="max-width:320px"
              color="grey"
              outlined
              dense
              hide-details
              prepend-icon="mdi-search"
              item-text="vendorName"
              item-value="vendorName"
              :show-select="true"
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:item.id="{ item }">
          <v-btn
            fab
            x-small
            class="elevation-1"
            :to="{ name: 'VendorUpdate', params: { id: item.id } }"
          >
            <v-icon color="success">mdi-pencil</v-icon></v-btn
          >
        </template>
      </v-data-table>
    </v-sheet>
    <router-view></router-view>
  </div>
</template>

<script>
import navbarMixins from "@/mixins/navbarMixins.js";
import notiMixin from "@/mixins/notiMixin.js";
import vendorService from "@/services/vendorService.js";

export default {
  mixins: [navbarMixins, notiMixin],
  data() {
    return {
      pageTitle: "Vendors",
      selected: [],
      search: "",
      items: [],
      tblHeaders: [
        {
          text: "Vendor Name",
          value: "vendorName",
          sortable: true,
          align: "left"
        },
        {
          text: "Location",
          value: "location",
          sortable: true,
          align: "left",
          width: "300px"
        },
        {
          text: "Contact #",
          value: "contactNum",
          sortable: true,
          align: "left",
          width: "100px"
        },
        {
          text: "action",
          value: "id",
          align: "right"
        }
      ]
    };
  },
  async created() {
    try {
      const response = await vendorService.list();
      this.items = response.data;
    } catch (error) {
      console.log(error);
    }
  },
  methods: {
    async remove() {
      if (this.selected.length == 0) return;

      const noti = {
        visible: true
      };

      try {
        let params = this.selected.map(a => a.id);
        await vendorService.delete(params);
        noti.type = "success";
        noti.content = `${params.length} ${
          params.length == 1 ? "record" : "records"
        } deleted successfully`;
      } catch (error) {
        noti.type = "error";
        this.internalErrorMsg = error;
        noti.content = error;
      } finally {
        this.showNotification(noti);
      }
    }
  }
};
</script>

<style></style>
