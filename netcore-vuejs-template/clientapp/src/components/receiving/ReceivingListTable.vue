<template>
  <div v-show="!loading">
    <v-data-table
      v-model="checkedItems"
      class="details-table"
      item-key="id"
      :headers="tblHeader"
      :items="items"
      show-select
      :hide-default-footer="true"
      :page.sync="pagination.page"
      :items-per-page="pagination.itemsPerPage"
      @page-count="pagination.pageCount = $event"
    >
      <template v-slot:top>
        <div>
          <v-toolbar
            flat
            style="border:1px solid #e0e0e0"
            class="children-mr-2"
          >
            <ButtonDelete label="Delete Selected" @click="bulkRemove()" />
            <ButtonAdd label="Create New" :to="{ name: 'ReceivingCreate' }" />
            <v-spacer></v-spacer>
            <div class="text--right">
              <Pagination
                v-model="pagination.page"
                :page-count="pagination.pageCount"
                :page-size="pagination.itemsPerPage"
                :item-count="items.length"
              />
            </div>
          </v-toolbar>
        </div>
      </template>
      <template v-slot:no-data>
        <div class="tbl-no-data">
          <div class="tbl-no-data__text">No Data Yet</div>
        </div>
      </template>
      <template v-slot:item.tDate="{ item }">
        {{ item.tDate }}
      </template>
      <template v-slot:item.pending="{ item }">
        <TransTableStatusChip :status="item.status" />
      </template>
      <template v-slot:item.action="{ item }">
        <TransTableActionColumn
          :edit-action="editAction(item)"
          :delete-action="deleteAction(item)"
        />
      </template>
      <template v-slot:footer>
        <div></div>
      </template>
    </v-data-table>
    <MessageBox ref="messagebox" />
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  name: "ReceivingListTable",
  data() {
    return {
      loading: true,
      checkedItems: [],
      selectedItem: {},
      tblHeader: [
        {
          text: "id",
          align: "start",
          sortable: false,
          value: "id",
          class: "font-weight-bold",
          width: 80
        },
        {
          text: "Date & Time",
          align: "start",
          sortable: true,
          value: "tDate",
          width: 220
        },
        {
          text: "PO Number",
          align: "start",
          sortable: true,
          value: "poNum",
          width: 120
        },
        {
          text: "DR Number",
          align: "start",
          sortable: true,
          value: "drNum",
          width: 120
        },
        {
          text: "Status",
          align: "center",
          sortable: false,
          value: "pending"
        },
        {
          text: "",
          align: "right",
          sortable: false,
          value: "action"
        }
      ],
      pagination: {
        page: 1,
        pageCount: 0,
        itemsPerPage: 50
      }
    };
  },
  computed: {
    ...mapState("receiving", {
      items: state => state.receivingList
    })
  },
  async created() {
    try {
      await this.list();
      this.loading = false;
    } catch (err) {
      console.log(err);
    }
  },
  methods: {
    ...mapActions("receiving", {
      list: "listReceiving",
      remove: "removeReceivings"
    }),
    bulkRemove() {
      try {
        if (this.checkedItems.length == 0) return;

        const msgBoxOpts = {
          title: "Delete Confirmation",
          content: `Do you want to delete selected items(${this.checkedItems.length})`,
          buttons: "deleteCancel"
        };
        this.$refs.messagebox.open(msgBoxOpts, () => {
          return this.remove(this.checkedItems).then(() => {
            this.checkedItems = [];
          });
        });
      } catch (error) {
        console.log(error);
      }
    },
    editAction(item) {
      const id = item.id;
      const r = {
        name: "ReceivingUpdate",
        params: { id: id }
      };
      return r;
    },
    deleteAction(item) {
      const id = item.id;
      return {
        name: "ReceivingDelete",
        params: { id: id }
      };
    }
  }
};
</script>

<style lang="scss"></style>
