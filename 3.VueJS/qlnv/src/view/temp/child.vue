<template>
  <div>
    <label class="col-label"
      >{{ mydata }}
      <span v-if="required == 'true'"
        >(<span class="required">*</span>)</span
      ></label
    >
    <div class="inputbox">
      <input
        class="field-input"
        :type="inputType"
        :id="id"
        :FieldName="fieldName"
        :tabindex="tabIndex"
        :datatype="dataType"
        :value="mydata"
        :placeholder="placeHolder"
        :required="required == 'true' ? true : false"
        @input="inputting"
        v-model="employee.FullName"
      />
      <div v-if="inputType != 'date'" class="clearchoice"></div>
      <div class="tooltiptext"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Child",
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
    employee:Object,
  },
  data() {
    return {
      modelData: "",
    };
  },
  methods: {
    /**
     * Vue chỉ dùng để thử, hoạt động, 
     */
    /**
     * Vue chỉ dùng để thử, hoạt động, 
     */
    inputting() {

      this.$emit("inputting", this.modelData, this.fieldName, this.employee);
    },
  },
  watch: {
    mydata: function () {
      console.log("data:", this.modelData, "value", this.mydata);
      // this.modelData= this.mydata
    },
  },
};
</script>