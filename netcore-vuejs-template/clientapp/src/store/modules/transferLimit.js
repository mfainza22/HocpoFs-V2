import transferLimitService from "../../services/transferLimitService";
import Vue from "vue";
export default {
  namespaced: true,
  state: {
    selectedDate: "",
    transferLimitList: []
  },
  getters: {
    default() {
      return {};
    },
    getShiftDay: state => item => {
      return {
        index: state.transferLimitList.indexOf(item),
        EffectiveDate: item.EffectiveDate,
        RawMaterialId: item.RawMaterialId,
        ComputedLimitKg: item.LimitShift1,
        AdjRemarks: item.AdjRemarksShift1,
        LimitStatus: item.LimitStatusShift1,
        ShiftId: 1
      };
    },
    getShiftNight: state => item => {
      return {
        index: state.transferLimitList.indexOf(item),
        EffectiveDate: item.EffectiveDate,
        RawMaterialId: item.RawMaterialId,
        ComputedLimitKg: item.LimitShift2,
        AdjRemarks: item.AdjRemarksShift2,
        LimitStatus: item.LimitStatusShift2,
        ShiftId: 2
      };
    }
  },
  actions: {
    async listTransferLimit({ commit, state }, dt) {
      try {
        if (dt == null) {
          dt = state.selectedDate;
        }
        console.log(dt);

        const response = await transferLimitService.list(dt);
        commit("SET_SELECTEDDATE", dt);
        commit("LIST_TRANSFERLIMITS", response.data);
      } catch (err) {
        console.log(err);
      }
    },
    async updateLimit({ commit }, item) {
      await transferLimitService.update(item);
      commit("CHANGE_LIMIT", item);
    }
  },
  mutations: {
    LIST_TRANSFERLIMITS(state, payload) {
      state.transferLimitList = payload;
    },
    CHANGE_LIMIT(state, payload) {
      switch (payload.ShiftId) {
        case 1:
          state.transferLimitList[payload.index].LimitShift1 =
            payload.ComputedLimitKg;
          break;
        case 2:
          state.transferLimitList[payload.index].LimitShift2 =
            payload.ComputedLimitKg;
          break;
      }
      Vue.set(state, "transferLimitList", state.transferLimitList);
    },

    SET_SELECTEDDATE(state, payload) {
      state.selectedDate = payload;
    }
  }
};
