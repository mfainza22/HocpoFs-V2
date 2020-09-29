const ts = Object.freeze({
  PENDING: "PENDING",
  COMPLETED: "COMPLETED",
  ALL: "ALL"
});

const tp = Object.freeze({
  NONE: "NONE",
  WEIGH_IN: "WEIGH_IN",
  WEIGH_OUT: "WEIGH_OUT",
  UPDATE_IN: "UPDATE_IN",
  UPDATE_OUT: "UPDATE_OUT",
  DELETE: "DELETE"
});
const tt = Object.freeze({
  IN: "IN",
  OUT: "OUT",
  ALL: "ALL"
});

export const enums = {
  transactionStatus: ts,
  transactionProcess: tp,
  ticketType: tt
};
