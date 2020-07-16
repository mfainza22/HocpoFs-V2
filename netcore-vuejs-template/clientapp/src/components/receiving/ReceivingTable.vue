<template>
  <div>
    <v-data-table
      v-model="checkedItems"
      class="details-table"
      item-key="id"
      :headers="tblHeader"
      :items="items"
      show-select
      :hide-default-footer="true"
      disable-pagination
    >
      <template v-slot:top>
        <div>
          <v-toolbar flat style="border:1px solid #e0e0e0">
            <ButtonDelete label="Delete Selected" @click="bulkRemoveItems()">
            </ButtonDelete>
            <v-spacer></v-spacer>
            <AutocompleteProduct
              v-model="selectedItem"
              placeholder="Search
            product"
              style="max-width:280px"
              prepend-inner-icon="mdi-magnify"
              @input="selectItem()"
            />
          </v-toolbar>
        </div>
      </template>
      <template v-slot:no-data>
        <div class="tbl-no-data">
          <div class="tbl-no-data__text">No Data Yet</div>
        </div>
      </template>
      <template v-slot:item.description="{ item }">
        <div>
          <div class="body-2 ">
            {{ item.description }}
          </div>
          <div class="caption font-weight-light">#{{ item.serialCode }}</div>
        </div>
      </template>
      <template v-slot:item.onStock="{ item }">
        <div>
          <span v-show="item.onStock > 0">{{ item.onStock }}</span>
          <span v-show="item.onStock == 0" class="error--text">
            N/A
          </span>
        </div>
      </template>
      <template v-slot:item.qty="{ item }">
        <v-edit-dialog :return-value.sync="item.qty" large>
          <div
            style="border: 1px solid #e0e0e0; width:100px; text-align: center"
            class="details-qty px-6 py-1"
          >
            {{ item.qty }}
          </div>
          <template v-slot:input>
            <v-text-field
              v-model="item.qty"
              label="Edit Quantity"
              autofocus
              type="number"
            >
            </v-text-field>
          </template>
        </v-edit-dialog>
      </template>
      <template v-slot:footer="{ props }">
        <div>
          <TransTableFooter
            :props="props"
            :tot-item="itemsCount"
            :tot-qty="itemsTotQty"
          />
        </div>
      </template>
    </v-data-table>
    <MessageBox ref="messagebox" />
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      checkedItems: [],
      loading: false,
      tblHeader: [
        {
          text: "Product",
          align: "start",
          sortable: false,
          value: "description",
          class: "font-weight-bold"
        },
        {
          text: "On Stock",
          align: "center",
          sortable: false,
          value: "onStock",
          width: "100px"
        },
        {
          text: "quantity",
          align: "center",
          sortable: false,
          value: "qty",
          editable: true,
          width: "100px"
        },
        {
          text: "",
          align: "center",
          value: "actions",
          sortable: false,
          width: "100px"
        }
      ],
      selectedItem: null
    };
  },
  computed: {
    ...mapGetters("receiving", {
      items: "itemsByDateAdded",
      itemsTotQty: "itemsTotQty",
      itemsCount: "itemsCount"
    })
  },
  created() {},
  methods: {
    ...mapActions("receiving", {
      addItem: "addReceivingItem",
      removeItems: "removeItems"
    }),
    ...mapActions("modalstatus", {
      showModal: "show"
    }),
    bulkRemoveItems() {
      try {
        if (this.checkedItems.length == 0) return;

        const msgBoxOpts = {
          title: "Delete Confirmation",
          content: `Do you want to delete selected items(${this.checkedItems.length})`,
          buttons: "deleteCancel"
        };
        this.$refs.messagebox.open(msgBoxOpts, () => {
          return this.removeItems(this.checkedItems).then(() => {
            this.checkedItems = [];
          });
        });
      } catch (error) {
        console.log(error);
      }
    },
    selectItem() {
      this.addItem(this.selectedItem);
    }
  }
};
</script>

<style lang="scss">
.v-data-table__empty-wrapper {
  td {
    padding: 0 !important;
  }
}
.tbl-no-data {
  height: 200px;
  display: flex;
  flex-direction: columns;
  justify-content: center;
  align-items: center;
  background-color: #f5f5f5;
}
</style>
