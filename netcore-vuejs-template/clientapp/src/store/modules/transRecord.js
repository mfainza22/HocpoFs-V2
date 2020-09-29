import transRecordService from "@/services/transRecordService.js";
import transferLimitService from "@/services/transferLimitService.js";
import { enums } from "@/helpers/constants.js";

export const namespaced = true;

export const state = {
  transRecord: {},
  transRecordValidationResult: {},
  transRecordList: [],
  transferLimit: {},
  transRecordFilter: {}
};

export const getters = {
  defaultItem: () => {
    const r = {
      TransactionId: 0,
      DTInbound: null,
      DTOutbound: null,
      ReceiptNum: "------",
      ControlNum: "------",
      PalletNum: "",
      InboundWt: 2737,
      OutboundWt: 0,
      GrossWt: 0.0,
      TareWt: 0.0,
      NetWt: 0.0,
      WtPerPackage: 50,
      WtPerPackageActual: 0,
      EmptyPackageWt: 0,
      TotEmptyPackWt: 0,
      ActualNetWt: 0,
      MinActualWt: 0,
      MaxActualWt: 0,
      RawMaterialId: 0,
      RawMaterialDesc: "",
      Quantity: 54,
      PackagingTypeId: 0,
      PackagingTypeDesc: null,
      BinNum: "",
      BinLocDesc: "",
      LocationId: 0,
      LocationName: "",
      WeigherInName: "",
      ShiftId: 0,
      ShiftDesc: null,
      TransactionProcess: "WEIGH_IN",
      TransactionStatus: "ALL",
      WeightStatus: "NONE",
      OfflineIn: false,
      OfflineOut: false,
      IsOffline: false,
      OnlineWt: 0,
      OfflineWt: 0,
      DTOfflineDate: null,
      TolActualWt: 0.3
    };

    return {
      ...r
    };
  }
};

export const actions = {
  handleError({ commit, dispatch }, error) {
    const noti = {
      visible: true,
      type: "error",
      content: error,
      icon: "mdi-exclamation-thick"
    };
    if (error.response.status == 400) {
      commit("STORE_SERVER_VALIDATION_RESULT", JSON.parse(error.response.data));
      // const serverValResult = Object.values()
      //   .join("\n")
      //   .split("\n");

      // const lis = serverValResult.map(a => `<li>${a}</li>`).join("");
      // const ul = `<ul class="text-right" style='list-style-type:none'>${lis}</ul>`;
      // console.log(ul);
      // noti.icon = "";
      // noti.content = ul;
      noti.content = "Transaction failed. Please check your details.";
    }
    dispatch("notification/show", noti, { root: true });
  },
  /**
   * Get record from the API
   * It validate is user has an access to get a record based on it transaction status
   * Some user has no right to access PENDING or COMPLETED Transaction
   * @param {Object} args
   * @param {string} args.transactionProcess transaction process passed from route meta
   * @param {string} args.transactionStatus transaction status passed from route meta
   * @param {string} args.id unique id of transaction record to process
   * @return {Object} return transaction record
   */
  async getTransRecord({ commit, getters }, args) {
    try {
      let response = null;
      let t = Object.assign({});
      t = Object.assign({}, getters.defaultItem);

      if (args.transactionProcess != enums.transactionProcess.WEIGH_IN) {
        if (args.transactionStatus == enums.transactionStatus.PENDING) {
          response = await transRecordService.getPending(args.id);
        } else {
          response = await transRecordService.getCompleted(args.id);
        }
        t = response.data;
      }

      if (t) {
        t.TransactionProcess = args.transactionProcess;
        t.TransactionStatus = args.transactionStatus;
      }

      commit("STORE_TRANSRECORD", t);
      return t;
    } catch (error) {
      throw new Error(error);
    }
  },
  /**
   *
   * @param {Object} {} vuex
   * @param {Object} obj transaction records
   *
   * @returns {Boolean} returns true if success else false
   */
  async save({ dispatch }, obj) {
    try {
      const tp = obj.TransactionProcess;
      if (tp == enums.transactionProcess.WEIGH_IN) {
        await dispatch("weighIn", obj);
      } else if (tp == enums.transactionProcess.WEIGH_OUT) {
        await dispatch("weighOut", obj);
      } else if (tp == enums.transactionProcess.UPDATE_IN) {
        await dispatch("updateIn", obj);
      } else if (tp == enums.transactionProcess.UPDATE_OUT) {
        await dispatch("updateOut", obj);
      }
    } catch (error) {
      dispatch("handleError", error);
      throw error;
    }
  },
  async weighIn({ commit, dispatch }, obj) {
    try {
      const r = await transRecordService.weighIn(obj);

      const noti = {
        visible: true,
        type: "success",
        content: "<span class='subtitle-1'>Weigh-In Complete</span>",
        icon: "mdi-check"
      };
      dispatch("notification/show", noti, { root: true });

      commit("STORE_TRANSRECORD", r);
    } catch (error) {
      throw new Error(error);
    }
  },
  async weighOut({ commit, dispatch }, obj) {
    let r = null;
    r = await transRecordService.weighOut(obj);
    const noti = {
      visible: true,
      type: "success",
      content: "<span class='subtitle-1'>Weigh-Out Complete</span>",
      icon: "mdi-check"
    };
    dispatch("notification/show", noti, { root: true });

    commit("STORE_TRANSRECORD", r);
  },
  async checkLimit({ commit, dispatch }, obj) {
    try {
      if (obj.MaterialId == 0) return;
      const r = await transferLimitService.checkLimit(obj);
      if (r.data == "") r.data = null;
      commit("STORE_LIMITINFO", r.data);
      commit("SET_TRANS_LIMITINFO", r.data);
      return r.data;
    } catch (error) {
      dispatch("handleError", error);
    }
  },
  /**
   *
   * @param {Object} obj filterOptions for getting the list of transactions
   * @returns {Array} Array of Object containing the list of transactions
   */
  async list({ commit, dispatch }, obj) {
    try {
      const r = await transRecordService.list(obj);
      commit("STORE_TRANSRECORDLIST", r.data);
    } catch (error) {
      dispatch("handleError", error);
      throw error;
    }
  }
};

export const mutations = {
  STORE_TRANSRECORD(state, payload) {
    state.transRecord = payload;
  },
  STORE_TRANSRECORDLIST(state, payload) {
    state.transRecordList = payload;
  },
  STORE_SERVER_VALIDATION_RESULT(state, payload) {
    state.transRecordValidationResult = payload;
  },
  SET_TRANS_LIMITINFO(state, payload) {
    state.transRecord.TransferLimitId = payload?.TransferLimitId;
  },
  STORE_LIMITINFO(state, payload) {
    state.transferLimit = payload;
  }
};
