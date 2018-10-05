import update from "immutability-helper";

const faker = require("faker");
const ReactDataGrid = require("react-data-grid");
const React = require("react");
const { Editors, Toolbar, Formatters } = require("react-data-grid-addons");
const { AutoComplete: AutoCompleteEditor, DropDownEditor } = Editors;
const { ImageFormatter } = Formatters;

faker.locale = "en_GB";

const counties = [
  { id: 0, title: "Bedfordshire" },
  { id: 1, title: "Berkshire" },
  { id: 2, title: "Buckinghamshire" },
  { id: 3, title: "Cambridgeshire" },
  { id: 4, title: "Cheshire" },
  { id: 5, title: "Cornwall" },
  { id: 6, title: "Cumbria, (Cumberland)" },
  { id: 7, title: "Derbyshire" },
  { id: 8, title: "Devon" },
  { id: 9, title: "Dorset" },
  { id: 10, title: "Durham" },
  { id: 11, title: "Essex" },
  { id: 12, title: "Gloucestershire" },
  { id: 13, title: "Hampshire" },
  { id: 14, title: "Hertfordshire" },
  { id: 15, title: "Huntingdonshire" },
  { id: 16, title: "Kent" },
  { id: 17, title: "Lancashire" },
  { id: 18, title: "Leicestershire" },
  { id: 19, title: "Lincolnshire" },
  { id: 20, title: "Middlesex" },
  { id: 21, title: "Norfolk" },
  { id: 22, title: "Northamptonshire" },
  { id: 23, title: "Northumberland" },
  { id: 24, title: "Nottinghamshire" },
  { id: 25, title: "Northamptonshire" },
  { id: 26, title: "Oxfordshire" },
  { id: 27, title: "Northamptonshire" },
  { id: 28, title: "Rutland" },
  { id: 29, title: "Shropshire" },
  { id: 30, title: "Somerset" },
  { id: 31, title: "Staffordshire" },
  { id: 32, title: "Suffolk" },
  { id: 33, title: "Surrey" },
  { id: 34, title: "Sussex" },
  { id: 35, title: "Warwickshire" },
  { id: 36, title: "Westmoreland" },
  { id: 37, title: "Wiltshire" },
  { id: 38, title: "Worcestershire" },
  { id: 39, title: "Yorkshire" }
];

const titles = ["Dr.", "Mr.", "Mrs.", "Miss", "Ms."];

export default class CreateTable extends React.Component {
  constructor(props, context) {
    super(props, context);

    this.state = {
      rows: this.createRows(5),
      columns: [
        {
          key: "id",
          name: "ID",
          resizable: true
        },
        {
          key: "count",
          name: "count",
          editable: true,
          resizable: true
        }
      ]
    };
  }

  createRows = numberOfRows => {
    let rows = [];
    for (let i = 0; i < numberOfRows; i++) {
      rows[i] = this.createFakeRowObjectData(i);
    }
    return rows;
  };

  createFakeRowObjectData = index => {
    return {
      id: "id_" + index,
      count: index * 10
    };
  };

  handleGridRowsUpdated = ({ fromRow, toRow, updated }) => {
    let rows = this.state.rows.slice();

    for (let i = fromRow; i <= toRow; i++) {
      let rowToUpdate = rows[i];
      let updatedRow = update(rowToUpdate, { $merge: updated });
      rows[i] = updatedRow;
    }

    this.setState({ rows });
  };

  handleAddRow = ({ newRowIndex }) => {
    const newRow = {
      value: newRowIndex,
      id: "id",
      count: "10"
    };

    let rows = this.state.rows.slice();
    rows = update(rows, { $push: [newRow] });
    this.setState({ rows });
  };
  handleAddCol = () => {
    const col = {
      key: "new",
      name: "new"
    };
    let cols = this.state.columns.slice();
    cols.push(col);
    this.setState({ cols });
  };

  getRowAt = index => {
    if (index < 0 || index > this.getSize()) {
      return undefined;
    }

    return this.state.rows[index];
  };

  getSize = () => {
    return this.state.rows.length;
  };

  render() {
    return (
      <div>
        <button onClick={this.handleAddCol}>Add Column</button>
        <ReactDataGrid
          columns={this.state.columns}
          rowGetter={this.getRowAt}
          rowsCount={this.getSize()}
          onGridRowsUpdated={this.handleGridRowsUpdated}
          toolbar={
            <Toolbar
              onAddRow={this.handleAddRow}
              renderAddRowButton={this.handleAddCol}
            />
          }
          rowHeight={50}
          minHeight={500}
          rowScrollTimeout={200}
        />
      </div>
    );
  }
}
