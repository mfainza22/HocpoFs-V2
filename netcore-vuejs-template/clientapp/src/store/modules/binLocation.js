import binLocationService from "@/services/binLocationService.js";
import { objectUtil } from "@/helpers/func";

export const name = "binLocation";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      BinLocationId: 0,
      BinLocDesc: ""
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
  async getBinLocation({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await binLocationService.getById(id);
        r = r.data;
      }

      commit("STORE_BINLOCATION", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listBinLocation({ commit, dispatch }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await binLocationService.list();
        commit("STORE_BINLOCATION_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createBinLocation({ commit, dispatch }, obj) {
    try {
      const r = await binLocationService.create(obj);
      commit("ADD_BINLOCATION", r.data);
      dispatch("handleSuccess", "New Raw Material added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updateBinLocation({ commit, dispatch }, obj) {
    try {
      const r = await binLocationService.update(obj.BinLocationId, obj);
      commit("UPDATE_BINLOCATION", r.data);
      dispatch("handleSuccess", "New Raw Material updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deleteBinLocations({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.BinLocationId);
      await binLocationService.bulkDelete(params);

      commit("DELETE_BINLOCATIONS", obj);
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
  STORE_BINLOCATION(state, payload) {
    state.item = payload;
  },
  STORE_BINLOCATION_LIST(state, payload) {
    state.items = payload;
  },
  ADD_BINLOCATION(state, payload) {
    state.items.push(payload);
  },
  UPDATE_BINLOCATION(state, payload) {
    var si = state.items.find(a => a.BinLocationId == payload.BinLocationId);
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_BINLOCATIONS(state, payload) {
    payload.forEach(item => {
      let idx = state.items
        .map(a => a.BinLocationId)
        .indexOf(item.BinLocationId);
      console.log(idx);
      state.items.splice(idx, 1);
    });
  }
};
