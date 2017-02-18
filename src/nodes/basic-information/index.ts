import { IComponentOptions } from 'angular';

const template = require('./template.html');

export default class hgNodeBasicInfo {
  bindings: {[key: string]: string};
  templateUrl: any;
  controller: any;

  constructor() {
    this.bindings = {
      node: '=hgNode'
    }
    this.templateUrl = template;
    this.controller = hgNodeBasicInfoController;
  }
}

class hgNodeBasicInfoController {
  node: any;
}