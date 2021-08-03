<template>
  <div>
    <label class="col-label"
      >{{ labelText }}
      <span v-if="required == 'true'"
        >(<span class="required">*</span>)</span
      ></label
    >
    <div class="inputbox">
      <input
        :type="inputType"
        class="field-input"
        :id="id"
        :tabindex="tabIndex"
        :datatype="dataType"
        :placeholder="placeHolder"
        :required="required == 'true' ? true : false"
        @input="onInput($event.target.value)"
        v-model="modelData"
      />
      <div v-if="inputType != 'date'" class="clearchoice" :class="{'isnone': isEmpty}" @click="clearInputOnClick"></div>
      <div class="tooltiptext"></div>
    </div>
  </div>
</template>

<script>
// import { CommonFn } from "../../js/mixins";
export default {
  // mixins: [CommonFn],
  name: "BaseInputLabel",
  props: {
    labelText: String,
    inputType: String,
    id: String,
    fieldName: String,
    tabIndex: String,
    mydata: String,
    dataType: String,
    placeHolder: String,
    required: String,
  },
  data() {
    return {
      modelData:this.mydata,
      isEmpty:true
    };
  },
  methods: {
    /**
     * Khi input, emit sự kiện lên component cha cùng với giá trị hiện tại và tên của ô nhập dữ liệu
     * Nguyễn Hùng 30/07
     */
     onInput(value) {
      this.$emit("input", value);
      if(this.modelData.length >0) this.isEmpty= false
    },
    clearInputOnClick(){
      console.log("clearing")
      this.modelData="";
      this.isEmpty= true
    }
  },
  watch: {
    mydata: function () {
      try {
        this.modelData= this.mydata
      } catch (error) {
        console.log("invalid data")
      }
    },
  },
};
</script>