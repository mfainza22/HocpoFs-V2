export default {
  toCommaNumeral(value) {
    if (isNaN(value)) return 0;
    return parseInt(value, 10).toLocaleString("en-US", {
      minimumFractionDigits: 0
    });
  },
  toFixed(value, dec) {
    if (isNaN(value)) return 0;
    return value.toFixed(dec);
  },
  upperCase(value) {
    if (typeof value === undefined) return value;
    return value.toUpperCase();
  }
};
