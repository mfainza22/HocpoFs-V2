import packagingTypeService from "@/services/packagingTypeService.js";
import { objectUtil } from "@/helpers/func";

export const name = "packagingType";
export const namespaced = true;
export const state = {
  item: {},
  items: []
};

export const getters = {
  defaultItem: () => {
    return Object.freeze({
      PackagingTypeId: 0,
      PackagingTypeCode: "",
      PackagingTypeDesc: ""
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
  async getPackagingType({ commit, dispatch, getters }, id) {
    try {
      let r = {};
      if (id == 0) {
        r = JSON.parse(JSON.stringify(getters.defaultItem));
      } else {
        r = await packagingTypeService.getById(id);
        r = r.data;
      }

      commit("STORE_PACKAGINGTYPE", r);
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async listPackagingType({ commit, dispatch }, forceUpdate) {
    try {
      if (forceUpdate == true || objectUtil.empty(state.items)) {
        const r = await packagingTypeService.list();
        commit("STORE_PACKAGINGTYPE_LIST", r.data);
      }
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async createPackagingType({ commit, dispatch }, obj) {
    try {
      const r = await packagingTypeService.create(obj);
      commit("ADD_PACKAGINGTYPE", r.data);
      dispatch("handleSuccess", "New Packaging Type added successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async updatePackagingType({ commit, dispatch }, obj) {
    try {
      const r = await packagingTypeService.update(obj.PackagingTypeId, obj);
      commit("UPDATE_PACKAGINGTYPE", r.data);
      dispatch("handleSuccess", "Packaging Type updated successfully");
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  async deletePackagingTypes({ commit, dispatch }, obj) {
    try {
      if (obj == null || obj.length == 0) {
        throw new Error("Please select a record to delete.");
      }

      let params = obj.map(a => a.PackagingTypeId);
      await packagingTypeService.bulkDelete(params);

      commit("DELETE_PACKAGINGTYPES", obj);
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
  STORE_PACKAGINGTYPE(state, payload) {
    state.item = payload;
  },
  STORE_PACKAGINGTYPE_LIST(state, payload) {
    state.items = payload;
  },
  ADD_PACKAGINGTYPE(state, payload) {
    state.items.push(payload);
  },
  UPDATE_PACKAGINGTYPE(state, payload) {
    var si = state.items.find(
      a => a.PackagingTypeId == payload.PackagingTypeId
    );
    if (si == null) {
      state.items.push(payload);
    } else {
      const idx = state.items.indexOf(si);
      state.items.splice(idx, 1, payload);
    }
  },
  DELETE_PACKAGINGTYPES(state, payload) {
    payload.forEach(item => {
      let idx = state.items
        .map(a => a.PackagingTypeId)
        .indexOf(item.PackagingTypeId);
      console.log(idx);
      state.items.splice(idx, 1);
    });
  }
};
