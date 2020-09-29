import service from "@/services/service.js";
import { required, max, between, min_value } from "vee-validate/dist/rules";
import { extend } from "vee-validate";

const pattern = {
  regex_invalid_char: /^[a-zA-Z\sа-яА-ЯёЁ\-\\.\\,\\_\\:\\\\/\\[0-9]+$/g
};
extend("required", {
  ...required,
  message: "{_field_} can not be empty",
  async validate(value) {
    if (typeof value === undefined) return false;
    if (value == undefined) return false;
    if (value == null) return false;
    if (value == "") return false;
    if (typeof value == Number) {
      if (value <= 0) return false;
    }
    return true;
  }
});
extend("min_value", {
  ...min_value,
  message: "The {_field_}  must not be Zero(0)"
});
extend("max", {
  ...max,
  message: "The {_field_}  must have at least {length} characters"
});
extend("between", {
  ...between,
  message: "The {_field_} must be between {min} and {max}"
});
extend("invalid_char", {
  message: "The {_field_} must not contain an invalid characters.",
  async validate(value) {
    if (value == "") return true;
    return new RegExp(pattern.regex_invalid_char).test(value);
  }
});
extend("remote", {
  getMessage: field => "The " + field + " field already exists.",
  async validate(value, args) {
    // console.log(args);
    try {
      const body = JSON.stringify(args.obj);
      const r = await service.post(args.url, body);
      return r.data == true;
    } catch (err) {
      return false;
    }
  },
  params: ["url", "obj"]
});
