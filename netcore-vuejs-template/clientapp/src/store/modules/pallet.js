import palletService from "@/services/palletService.js";
import { objectUtil } from "@/helpers/func";

export const name = "pallet";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      PalletId: 0,
      PalletNum: ""
    });
  }
};

export const actions = {
  async handleError({ dispatch }, error) {
    const noti = {
      visible: true,
      type: "error",
      content: error,
      icon: "mdi-exclamation-thick"
    };
    dispatch("notification/show", noti, { root: true });
  },
  async handleSuccess({ dispatch }, content) {
    const noti = {
      visible: true,
      type: "success",
      content: content,
      icon: "mdi-check"
    };
    dispatch("notification/show", noti, { root: true });
  },
  async getPallet({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await palletService.getById(id);
        r = r.data;
      }

      commit("STORE_PALLET", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listPallet({ commit, dispatch }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await palletService.list();
        commit("STORE_PALLET_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createPallet({ commit, dispatch }, obj) {
    try {
      const r = await palletService.create(obj);
      commit("ADD_PALLET", r.data);
      dispatch("handleSuccess", "New Pallet added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updatePallet({ commit, dispatch }, obj) {
    try {
      const r = await palletService.update(obj.PalletId, obj);
      commit("UPDATE_PALLET", r.data);
      dispatch("handleSuccess", "Pallet updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deletePallet({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.PalletId);
      await palletService.bulkDelete(params);

      commit("DELETE_PALLETS", obj);
      dispatch(
        "handleSuccess",
        `${params.length} ${
          params.length == 1 ? "record" : "records"
        } deleted successfully`
      );
    } catch (error) {
      dispatch("handleError", error);
    }
  }
};

export const mutations = {
  STORE_PALLET(state, payload) {
    state.item = payload;
  },
  STORE_PALLET_LIST(state, payload) {
    state.items = payload;
  },
  ADD_PALLET(state, payload) {
    state.items.push(payload);
  },
  UPDATE_PALLET(state, payload) {
    var si = state.items.find(a => a.PalletId == payload.PalletId);
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_PALLETS(state, payload) {
    payload.forEach(item => {
      let idx = state.items.map(a => a.PalletId).indexOf(item.PalletId);
      console.log(idx);
      state.items.splice(idx, 1);
    });
  }
};
