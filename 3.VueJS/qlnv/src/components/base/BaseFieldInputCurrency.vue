<template>
  <div>
    <label>{{ labelText }}</label>
    <div class="money-input" :class="{'missing': isMissing , ':focus' : isFocus}">
      <input
        :id="id"
        :FieldName="fieldName"
        datatype="Number"
        class="field-input align-right money"
        maxlength="15"
        :tabindex="tabIndex"
        @input="onInput($event.target.value)"
        @focus="onFocus"
        v-model="modelData"
      />
      <div class="currency">({{ currency }})</div>
    </div>
  </div>
</template>
<script>
export default {
  name: "FieldInputIcon",
  data() {
    return {
      modelData: "",
      isEmpty: true,
    };
  },
  props: {
    subclass: String,
    iconName: String,
    labelText: String,
    id: String,
    fieldName: String,
    tabIndex: String,
    mydata: String,
    dataType: String,
    placeHolder: String,
    required: String,
    currency: String,
    isMissing: Boolean,
    isFocus: Boolean
  },
  methods: {
    /**
     * Khi input, emit sự kiện lên component cha cùng với giá trị hiện tại và tên của ô nhập dữ liệu
     * Nguyễn Hùng 30/07
     */
    onInput(value) {
      let formatedValue = this.formatNumber(value);
      this.$emit("input", formatedValue);
      if (this.modelData.length > 0) this.isEmpty = false;
    },
    clearInputOnClick() {
      console.log("clearing");
      this.modelData = "";
      this.isEmpty = true;
    },
    onFocus(){
      
    },

    /**
     *
     * @param {} myinput  xâu bất kỳ
     * @returns Xâu số (dấu ngăn . cách phần nghìn )
     */
    formatMoney(myinput) {
      myinput += "";
      if (myinput != null) {
        myinput.replaceAll(".", "");

        let onlynumber = "";
        for (var i = 0; i < myinput.length; i++) {
          if (!isNaN(parseInt(myinput[i], 10))) {
            onlynumber += myinput[i];
          }
        }
        return Number(onlynumber).toLocaleString("vi");
      }
      return 0;
    },
    /**
     * @param {} myinput Xâu  input bất kỳ
     * @returns Xâu chỉ chứa số
     *  Nguyễn Hùng 18/07
     */
    formatNumber(myinput) {
      myinput += "";
      if (myinput != null) {
        myinput.replaceAll(".", "");

        let onlynumber = "";
        for (var i = 0; i < myinput.length; i++) {
          if (!isNaN(parseInt(myinput[i], 10))) {
            onlynumber += myinput[i];
          }
        }
        return onlynumber;
      }
      return 0;
    },
  },
  watch: {
    mydata: function () {
      try {
        this.modelData = this.formatMoney(this.mydata);
      } catch (error) {
        console.log("invalid data");
      }
    },
  },
};
</script>