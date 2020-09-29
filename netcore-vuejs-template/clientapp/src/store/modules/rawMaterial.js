import rawMaterialService from "@/services/rawMaterialService.js";
import { objectUtil } from "@/helpers/func";

export const name = "rawMaterial";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      RawMaterialId: 0,
      RawMaterialCode: "",
      RawMaterialDesc: ""
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
  async getRawMaterial({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await rawMaterialService.getById(id);
        r = r.data;
      }

      commit("STORE_RAWMATERIAL", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listRawMaterial({ commit, dispatch, state }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await rawMaterialService.list();
        commit("STORE_RAWMATERIAL_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createRawMaterial({ commit, dispatch }, obj) {
    try {
      const r = await rawMaterialService.create(obj);
      commit("ADD_RAWMATERIAL", r.data);
      dispatch("handleSuccess", "New Raw Material added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updateRawMaterial({ commit, dispatch }, obj) {
    try {
      const r = await rawMaterialService.update(obj.RawMaterialId, obj);
      commit("UPDATE_RAWMATERIAL", r.data);
      dispatch("handleSuccess", "New Raw Material updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deleteRawMaterials({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.RawMaterialId);
      await rawMaterialService.bulkDelete(params);

      commit("DELETE_RAWMATERIALS", obj);
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
  STORE_RAWMATERIAL(state, payload) {
    state.item = payload;
  },
  STORE_RAWMATERIAL_LIST(state, payload) {
    state.items = payload;
  },
  ADD_RAWMATERIAL(state, payload) {
    state.items.push(payload);
  },
  UPDATE_RAWMATERIAL(state, payload) {
    var si = state.items.find(a => a.RawMaterialId == payload.RawMaterialId);
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_RAWMATERIALS(state, payload) {
    payload.forEach(item => {
      let idx = state.items
        .map(a => a.RawMaterialId)
        .indexOf(item.RawMaterialId);
      state.items.splice(idx, 1);
    });
  }
};
