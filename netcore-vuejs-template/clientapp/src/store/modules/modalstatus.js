export default {
  namespaced: true,
  state: {
    options: {}
  },
  getters: {
    default() {
      return {
        visible: false,
        type: "default",
        buttons: "default",
        content: "Modal Status Content"
      };
    },
    enumsType() {
      return {
        DEFAULT: "default",
        SUCCESS: "success",
        WARNING: "warning",
        ERROR: "error"
      };
    },
    enumsButtons() {
      return {
        OK: "ok",
        OK_CANCEL: "okCancel",
        YES_NO: "yesNo",
        SUBMIT_CANCEL: "submitCancel",
        RETRY_CANCEL: "retryCancel",
        DELETE_CANCEL: "deleteCancel",
        CUSTOM: "custom"
      };
    },
    enumsevent() {
      return {
        ok: "ok",
        yes: "yes",
        no: "no",
        cancel: "cancel",
        submit: "submit",
        delete: "delete",
        retry: "retry"
      };
    }
  },
  actions: {
    show({ commit }, cbP) {
      commit("SHOW");
      commit("SET_CALLBACK_PROCEED", cbP);
    },
    hide({ commit }) {
      commit("HIDE");
      commit("SET_CALLBACK_PROCEED", null);
    },
    async proceed({ state }) {
      if (state.callBackProceed == null) return false;

      return await state.callBackProceed();
    },
    clickEvent(e) {
      console.log(e);
    }
  },
  mutations: {
    SHOW(state) {
      state.visible = true;
    },
    HIDE(state) {
      state.visible = false;
    },
    SET_CALLBACK_PROCEED(state, payload) {
      state.callBackProceed = payload;
    }
  }
};
