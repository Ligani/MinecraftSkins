import React from 'react';

export default function PurchaseRow({ item }) {

  const date = item.purchasedAtUtc 
    ? new Date(item.purchasedAtUtc).toLocaleString() 
    : '---';

  return (
    <tr>
      <td>
        <div>{item.skinName || 'Скин'}</div>
        <div>
          ID: {item.id?.substring(0, 8)}...
        </div>
      </td>
      <td >{date}</td>
      <td>
        <div>
          ₿ {item.btcPrice?.toFixed(8)}
        </div>
      </td>
      <td>
        ${item.btcUsdRate?.toLocaleString()}
      </td>
    </tr>
  );
}