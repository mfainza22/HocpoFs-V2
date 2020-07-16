import receivingService from "@/services/receivingService.js";
import vendorService from "@/services/vendorService";
// import { getField, updateField } from "vuex-map-fields";
import Vue from "vue";

const dd = {
  id: 0,
  tDate: null,
  tTime: "",
  vendor: "",
  poNum: "",
  drNum: "",
  preparedById: "",
  items: [],
  pending: true
};

// const defaultItem = {
//   productId: 0,
//   description: "",
//   serialCode: "",
//   category: "",
//   brand: "",
//   uom: "",
//   initStock: 0,
//   stock: 0,
//   qty: 0,
//   img: ""
// };

export default {
  namespaced: true,
  state: {
    receiving: {},
    receivingList: []
  },
  getters: {
    defaultReceiving() {
      return {
        id: 0,
        tDate: null,
        tTime: "",
        vendor: "",
        poNum: "",
        drNum: "",
        preparedById: "",
        items: [],
        pending: true
      };
    },
    itemsTotQty(state) {
      if (state.receiving.items == null) return 0;
      if (state.receiving.items.length == 0) return 0;
      return state.receiving.items.reduce(function(a, b) {
        return parseInt(a) + parseInt(b.qty);
      }, 0);
    },
    itemsCount(state) {
      return state.receiving.items.length;
    },
    itemsByDateAdded(state) {
      return state.receiving.items.sort(function(o1, o2) {
        if (o1.dateAdded > o2.dateAdded) return -1;
        if (o1.dateAdded < o2.dateAdded) return 1;
        return 0;
      });
    }
  },
  actions: {
    async listReceiving({ commit }) {
      const response = await receivingService.list();
      const items = response.data;
      commit("STORE_LIST", items);
    },
    async initReceiving({ commit, getters }) {
      console.log(getters.defaultReceiving);
      commit("STORE_RECEIVING", getters.defaultReceiving);
    },
    async getReceivingById({ commit }, id) {
      try {
        const response = await receivingService.getById(id);
        commit("STORE_RECEIVING", dd);
        commit("STORE_RECEIVING", response.data);
        return response.data;
      } catch (error) {
        console.log(error);
      }
    },
    async removeReceivings({ commit }, items) {
      try {
        await receivingService.remove(items.map(a => a.id));
        commit("REMOVE_RECEIVINGS", items);
      } catch (error) {
        console.log(error);
      }
    },
    addReceivingItem({ state, commit, dispatch }, item) {
      const noti = {
        visible: true
      };

      // if (item.onStock === 0) {
      //   noti.type = "error";
      //   noti.content = `<span>${item.description}</span><span class="error--text text--lighten-4 ml-3 mr-3">Out of Stock</span>`;
      //   dispatch("notification/show", noti, { root: true });
      //   return;
      // }

      if (state.receiving.items.map(pi => pi.id).indexOf(item.id) <= -1) {
        Vue.set(item, "qty", 1);
        var dt = new Date();
        Vue.set(item, "dateAdded", dt);
        commit("ADD_ITEM", item);

        noti.type = "success";
        noti.content = `<span class="green--text text--lighten-3 mr-3">Added</span><span>${item.description}</span>`;
        dispatch("notification/show", noti, { root: true });
        return;
      }

      if (!item.serialized) {
        item.qty = parseInt(item.qty) + parseInt(1);
        commit("UPDATE_QTY", item);

        noti.type = "success";
        noti.content = `<span>${item.description}</span><span class="green--text text--lighten-4 font-weight-bold sub-title-1 ml-3" >+1</span>`;
      } else {
        noti.type = "warning";
        noti.content = `<span>${item.description}</span><span class="warning--text text--lighten-3 mr-3 text--">&nbsp;already added.</span>`;
      }
      dispatch("notification/show", noti, { root: true });
    },
    async createReceiving({ state, dispatch }) {
      try {
        receivingService.create(state.receiving);
        const noti = {
          visible: true,
          type: "success",
          content: "New receiving has been created."
        };
        dispatch("notification/show", noti, { root: true });
      } catch (error) {
        console.log(error);
      }
    },
    async updateReceiving({ state, dispatch }) {
      try {
        await receivingService.update(state.receiving);
        const noti = {
          visible: true,
          type: "success",
          content: "Record has been updated successfully."
        };
        dispatch("notification/show", noti, { root: true });
      } catch (error) {
        console.log(error);
      }
    },
    async removeItems({ commit, dispatch }, items) {
      if (items.length == 0) return;

      try {
        commit("REMOVE_ITEMS", items);

        let content = `<span class="green--text text--lighten-3 mr-3">Removed</span>`;
        items.map(a => {
          content += `<div>${a.description}</div>`;
        });
        const noti = {
          visible: true,
          type: "error",
          content: content
        };

        dispatch("notification/show", noti, { root: true });
        dispatch("modalstatus/hide", null, { root: true });

        return await true;
      } catch (error) {
        return error;
      }
    },
    async validateReceiving({ state, dispatch }) {
      const valErrors = [];
      if (state.receiving.items.length == 0) {
        valErrors.push("Please add at least 1 item to continue.");
      }

      if (typeof state.receiving.vendor !== "object") {
        valErrors.push("Vendor is required");
      } else {
        const vendor = await vendorService.getById(state.receiving.vendor.id);

        if (typeof vendor.data === "undefined") {
          valErrors.push("Specified vendor does not exists.");
        }
      }

      if (valErrors.length > 0) {
        var content = "";
        valErrors.map(m => {
          content += `<div>${m}</div>`;
        });

        const noti = {
          visible: true,
          type: "error",
          content: content
        };

        dispatch("notification/show", noti, { root: true });
        return false;
      }

      return true;
    },
    updateState({ commit }, item) {
      commit("UPDATE_RECEIVING_STATE", item);
    },
    async getNew() {}
  },
  mutations: {
    STORE_LIST(state, payload) {
      state.receivingList = payload;
    },
    STORE_RECEIVING(state, payload) {
      Vue.set(state, "receiving", payload);
    },
    REMOVE_RECEIVINGS(state, payload) {
      payload.forEach(item => {
        let idx = state.receivingList.map(a => a.id).indexOf(item.id);
        state.receivingList.splice(idx, 1);
      });
    },
    ADD_ITEM(state, payload) {
      state.receiving.items.push(payload);
    },
    REMOVE_ITEMS(state, payload) {
      payload.forEach(item => {
        let idx = state.receiving.items.map(a => a.id).indexOf(item.id);
        state.receiving.items.splice(idx, 1);
      });
      /* or use this approach
        import arrayUtils from "@/helpers/arrayUtils.js";
        arrayUtils.remove(this.receivingRecord.items, this.checkedItems, "id");
      */
    },
    UPDATE_ITEM_QTY(state, payload) {
      let si = state.receiving.items.filter(i => i.id == payload.id)[0];
      si.qty = payload.qty;
    },
    UPDATE_RECEIVING_STATE(state, payload) {
      state.receiving.vendor = payload.vendor;
      state.receiving.poNum = payload.poNum;
      state.receiving.drNum = payload.drNum;
    }
  }
};
